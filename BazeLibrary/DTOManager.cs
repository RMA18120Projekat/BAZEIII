using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using NHibernate;
using NHibernate.Linq.Functions;
using NHibernate.Proxy;
using NHibernate.Util;
using Skola.Entiteti;
using static Skola.OcenaPregled;

namespace Skola
{
    public class DTOManager
    {
        public static List<UcenikPregled> vratiSveUcenike()
        {
            List<UcenikPregled> listaKojuPrikazujemo = new List<UcenikPregled>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<Skola.Entiteti.UCenik> listaKojaPribavljaPodatkeIzBaze = from o in s.Query<Skola.Entiteti.UCenik>()
                                                                                     select o;
                int i = 0;
                foreach (Skola.Entiteti.UCenik podatakIzBaze in listaKojaPribavljaPodatkeIzBaze)
                {
                    if (podatakIzBaze.Jmbg == null || podatakIzBaze.Naziv_smera == null)
                        continue;
                    SmerPregled prikazReference = vratiSmer(podatakIzBaze.Naziv_smera.Naziv);
                    UcenikPregled clanListeKojuPrikazujemo = new UcenikPregled();
                    clanListeKojuPrikazujemo.Jmbg = podatakIzBaze.Jmbg;
                    clanListeKojuPrikazujemo.Ime = podatakIzBaze.Ime;
                    clanListeKojuPrikazujemo.Prezime = podatakIzBaze.Prezime;
                    clanListeKojuPrikazujemo.Adresa = podatakIzBaze.Adresa;
                    clanListeKojuPrikazujemo.Razred = podatakIzBaze.Razred;
                    clanListeKojuPrikazujemo.JUB = podatakIzBaze.Jub;

                    clanListeKojuPrikazujemo.NazivSmera = prikazReference;
                    clanListeKojuPrikazujemo.DatumUpisa = podatakIzBaze.DatumUpisaSmera;

                    foreach (Roditelj roditelj in podatakIzBaze.Roditelji)
                    {
                        RoditeljPregled roditeljPregled = new RoditeljPregled((int)roditelj.Id, roditelj.Ime, roditelj.Prezime, roditelj.Clan_veca, null);
                        clanListeKojuPrikazujemo.Roditelji.Add(roditeljPregled);
                    }
                    foreach (Predmet a in podatakIzBaze.Predmeti)
                    {
                        PredmetPregled b = new PredmetPregled(a.Ime, a.Godina);
                        clanListeKojuPrikazujemo.Predmeti.Add(b);
                    }
                    foreach (DobijaOcenu a in podatakIzBaze.DobijaOcenuIzPredmeta)
                    {
                        PredmetPregled predmetDobija = new PredmetPregled(a.Predmet.Ime, a.Predmet.Godina);
                        OcenaPregled ocenaDobija = new OcenaPregled(a.Ocena.ID, (int)a.Ocena.Vrednost, a.Ocena.tekstOpis);
                        DobijaOcenuPregled b = new DobijaOcenuPregled(a.ID, null, ocenaDobija, predmetDobija, a.Datum_upisa_ocene);
                        clanListeKojuPrikazujemo.Ocene.Add(b);
                    }

                    if (clanListeKojuPrikazujemo != null)
                        listaKojuPrikazujemo.Add(clanListeKojuPrikazujemo);



                    i++;

                }

                s.Close();
            }
            catch (Exception ec)
            {

            }

            return listaKojuPrikazujemo;
        }

        public static void dodajUcenika(UcenikPregled p)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.UCenik o = new Skola.Entiteti.UCenik();

                o.Ime = p.Ime;
                o.Prezime = p.Prezime;
                o.Jmbg = p.Jmbg;
                o.Adresa = p.Adresa;
                Smer smer = s.Load<Skola.Entiteti.Smer>(p.NazivSmera.NazivSmera);
                o.Naziv_smera = smer;
                o.DatumUpisaSmera = p.DatumUpisa;
                o.Jub = p.JUB;

                s.SaveOrUpdate(o);

                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
        }

        public static UcenikPregled azurirajUcenika(UcenikPregled p)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.UCenik o = s.Load<Skola.Entiteti.UCenik>(p.Jmbg);
                o.Ime = p.Ime;
                o.Prezime = p.Prezime;
                Skola.Entiteti.Smer zmer = s.Load<Skola.Entiteti.Smer>(o.Naziv_smera.Naziv);
                o.Adresa = p.Adresa;
                o.Naziv_smera = zmer;
                o.DatumUpisaSmera = p.DatumUpisa;

                s.Update(o);
                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }

            return p;
        }
        public static bool DodeliSmerUceniku(long Jmbg, string Smer)
        {
            try
            {
                UcenikPregled u = vratiUcenika(Jmbg);
                SmerPregled s = vratiSmer(Smer);
                u.NazivSmera = s;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool dodeliPredmetUceniku(string Predmet, long Ucenik)
        {
            try
            {

                ISession s = DataLayer.GetSession();
                UCenik o = s.Load<Skola.Entiteti.UCenik>(Ucenik);
                Predmet predmet = s.Load<Predmet>(Predmet);
                o.Predmeti.Add(predmet);
                predmet.Ucenici.Add(o);
                s.SaveOrUpdate(o);
                s.SaveOrUpdate(predmet);
                s.Flush();
                s.Close();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static UcenikPregled vratiUcenika(long jmbg)
        {
            UcenikPregled pb = new UcenikPregled();
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.UCenik o = s.Load<Skola.Entiteti.UCenik>(jmbg);
                SmerPregled smer = vratiSmer(o.Naziv_smera.Naziv);

                pb.Jmbg = o.Jmbg;
                pb.Ime = o.Ime;
                pb.Prezime = o.Prezime;
                pb.Adresa = o.Adresa;
                pb.Razred = o.Razred;
                pb.JUB = o.Jub;
                pb.NazivSmera = smer;
                pb.DatumUpisa = o.DatumUpisaSmera;
                foreach (Roditelj roditelj in o.Roditelji)
                {
                    RoditeljPregled roditeljPregled = new RoditeljPregled((int)roditelj.Id, roditelj.Ime, roditelj.Prezime, roditelj.Clan_veca, null);
                    pb.Roditelji.Add(roditeljPregled);
                }
                foreach (Predmet a in o.Predmeti)
                {
                    PredmetPregled b = new PredmetPregled(a.Ime, a.Godina);
                    pb.Predmeti.Add(b);
                }
                foreach (DobijaOcenu a in o.DobijaOcenuIzPredmeta)
                {

                    DobijaOcenuPregled ocenuDobija = new DobijaOcenuPregled(a.ID, new UcenikPregled(a.Ucenik.Jmbg, a.Ucenik.Ime, a.Ucenik.Prezime, a.Ucenik.Adresa, a.Ucenik.Razred, a.Ucenik.Jub, new SmerPregled(a.Ucenik.Naziv_smera.Naziv, (int)a.Ucenik.Naziv_smera.maksBrUcenika), a.Ucenik.DatumUpisaSmera), new OcenaPregled(a.Ocena.ID, (int)a.Ocena.Vrednost, a.Ocena.tekstOpis), new PredmetPregled(a.Predmet.Ime, a.Predmet.Godina), a.Datum_upisa_ocene);
                    pb.Ocene.Add(ocenuDobija);
                }



                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }

            return pb;
        }

        public static void obrisiUcenika(long jmbg)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.UCenik o = s.Load<Skola.Entiteti.UCenik>(jmbg);

                s.Delete(o);
                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
        }
        public static List<RoditeljPregled> vratiSveRoditelje()
        {
            List<RoditeljPregled> roditelj = new List<RoditeljPregled>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<Skola.Entiteti.Roditelj> sviRoditelji = from o in s.Query<Skola.Entiteti.Roditelj>()
                                                                    select o;
                int i = 0;
                foreach (Skola.Entiteti.Roditelj p in sviRoditelji)
                {
                    if (p.Id == null)
                        continue;
                    RoditeljPregled z = new RoditeljPregled();
                    z.ID = p.Id;
                    z.ClanVeca = p.Clan_veca;
                    z.Ime = p.Ime;
                    z.Prezime = p.Prezime;
                    UcenikPregled ucenik;
                    if (p.Ucenik == null)
                    {
                        ucenik = null;
                    }
                    else
                    {
                        SmerPregled smer = vratiSmer(p.Ucenik.Naziv_smera.Naziv);
                        ucenik = new UcenikPregled(p.Ucenik.Jmbg, p.Ucenik.Ime, p.Ucenik.Prezime, p.Ucenik.Adresa, p.Ucenik.Razred, p.Ucenik.Jub, smer, p.Ucenik.DatumUpisaSmera);
                        z.Ucenik = ucenik;
                    }
                    foreach (BrojTelefona a in p.brojeviTelefona)
                    {
                        BrojTelefonaPregled b = new BrojTelefonaPregled(a.vrednost, null);
                        z.Brojevi.Add(b);

                    }

                    if (z != null)
                        roditelj.Add(z);



                    i++;

                }

                s.Close();
            }
            catch (Exception ec)
            {

            }

            return roditelj;
        }
        public static RoditeljPregled vratiRoditelja(int id)
        {
            RoditeljPregled pb = new RoditeljPregled();
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Roditelj o = s.Load<Skola.Entiteti.Roditelj>(id);

                pb.ID = o.Id;
                pb.Ime = o.Ime;
                pb.Prezime = o.Prezime;
                pb.ClanVeca = o.Clan_veca;

                UcenikPregled ucenik;
                if (o.Ucenik == null)
                {
                    ucenik = null;
                }
                else
                {
                    SmerPregled smer = vratiSmer(o.Ucenik.Naziv_smera.Naziv);
                    ucenik = new UcenikPregled(o.Ucenik.Jmbg, o.Ucenik.Ime, o.Ucenik.Prezime, o.Ucenik.Adresa, o.Ucenik.Razred, o.Ucenik.Jub, smer, o.Ucenik.DatumUpisaSmera);
                    pb.Ucenik = ucenik;
                }

                foreach (BrojTelefona broj in o.brojeviTelefona)
                {
                    BrojTelefonaPregled brojPregled = new BrojTelefonaPregled(broj.vrednost, null);
                    pb.Brojevi.Add(brojPregled);

                }


                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }

            return pb;
        }
        public static bool DodeliUcenikaRoditelju(int id, long Jmbg)
        {
            UcenikPregled ucenik = vratiUcenika(Jmbg);
            RoditeljPregled r = vratiRoditelja(id);
            r.Ucenik = ucenik;
            return true;
        }

        public static void dodajRoditelja(RoditeljPregled p)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Roditelj o = new Skola.Entiteti.Roditelj();

                o.Ime = p.Ime;
                o.Prezime = p.Prezime;
                o.Id = p.ID;
                UCenik a = s.Load<UCenik>(p.Ucenik.Jmbg);
                o.Ucenik = a;
                o.Clan_veca = p.ClanVeca;

                s.Save(o);

                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
        }

        public static RoditeljPregled azurirajRoditelja(RoditeljPregled p)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Roditelj o = s.Load<Skola.Entiteti.Roditelj>(p.ID);
                o.Ime = p.Ime;
                o.Prezime = p.Prezime;

                o.Clan_veca = p.ClanVeca;

                s.Update(o);
                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }

            return p;
        }



        public static void obrisiRoditlja(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Roditelj o = s.Load<Skola.Entiteti.Roditelj>(id);

                s.Delete(o);
                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
        }
        //////////////////////////////////
        ///
        public static List<PredmetPregled> vratiSvePredmete()
        {
            List<PredmetPregled> predmetiPregled = new List<PredmetPregled>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<Skola.Entiteti.Predmet> sviPredmeti = from o in s.Query<Skola.Entiteti.Predmet>()
                                                                  select o;
                int i = 0;
                foreach (Skola.Entiteti.Predmet p in sviPredmeti)
                {
                    if (p.Ime == null)
                        continue;
                    PredmetPregled pregled = new PredmetPregled();

                    pregled.ImePredmeta = p.Ime;
                    pregled.Godina_studija = p.Godina;
                    foreach (UCenik ucenik in p.Ucenici)
                    {
                        pregled.Ucenici.Add(new UcenikPregled(ucenik.Jmbg, ucenik.Ime, ucenik.Prezime, ucenik.Adresa, ucenik.Razred, ucenik.Jub, new SmerPregled(ucenik.Naziv_smera.Naziv, (int)ucenik.Naziv_smera.maksBrUcenika), ucenik.DatumUpisaSmera));
                    }
                    foreach (Angazovan angazovan in p.Nastavnici)
                    {
                        ZaposleniPregled pom1 = vratiZaposlenig(angazovan.Zaposlen.Jmbg);

                        pregled.Angazovani.Add(new AngazovanPregled(angazovan.Id, angazovan.DatumOd, angazovan.DatumDo, pom1, null));
                    }
                    foreach (DobijaOcenu dobijaOcenu in p.DobijaUcenikOcenu)
                    {
                        pregled.Ocene.Add(new DobijaOcenuPregled(dobijaOcenu.ID, new UcenikPregled(dobijaOcenu.Ucenik.Jmbg, dobijaOcenu.Ucenik.Ime, dobijaOcenu.Ucenik.Prezime, dobijaOcenu.Ucenik.Adresa, dobijaOcenu.Ucenik.Razred, dobijaOcenu.Ucenik.Jub, new SmerPregled(dobijaOcenu.Ucenik.Naziv_smera.Naziv, (int)dobijaOcenu.Ucenik.Naziv_smera.maksBrUcenika), dobijaOcenu.Ucenik.DatumUpisaSmera), new OcenaPregled(dobijaOcenu.Ocena.ID, (int)dobijaOcenu.Ocena.Vrednost, dobijaOcenu.Ocena.tekstOpis), null, dobijaOcenu.Datum_upisa_ocene));


                    }


                    if (pregled != null)
                        predmetiPregled.Add(pregled);



                    i++;

                }

                s.Close();
            }
            catch (Exception ec)
            {

            }

            return predmetiPregled;
        }
        public static bool dodeliUcenikaPredmetu(long Ucenik, string Predmet)
        {
            try
            {

                ISession s = DataLayer.GetSession();
                UCenik o = s.Load<Skola.Entiteti.UCenik>(Ucenik);
                Predmet predmet = s.Load<Predmet>(Predmet);
                o.Predmeti.Add(predmet);
                predmet.Ucenici.Add(o);
                s.SaveOrUpdate(o);
                s.SaveOrUpdate(predmet);
                s.Flush();
                s.Close();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public static void dodajPredmet(PredmetPregled p)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Predmet o = new Skola.Entiteti.Predmet();

                o.Ime = p.ImePredmeta;
                o.Godina = p.Godina_studija;

                s.SaveOrUpdate(o);

                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
        }

        public static PredmetPregled azurirajPredmet(PredmetPregled p)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Predmet o = s.Load<Skola.Entiteti.Predmet>(p.ImePredmeta);
                o.Ime = p.ImePredmeta;
                o.Godina = p.Godina_studija;
                s.Update(o);
                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }

            return p;
        }

        public static PredmetPregled vratiPredmet(string ime)
        {
            PredmetPregled pb = new PredmetPregled();
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Predmet o = s.Load<Skola.Entiteti.Predmet>(ime);
                PredmetPregled pregled = new PredmetPregled();

                pregled.ImePredmeta = o.Ime;
                pregled.Godina_studija = o.Godina;
                foreach (UCenik ucenik in o.Ucenici)
                {
                    pregled.Ucenici.Add(new UcenikPregled(ucenik.Jmbg, ucenik.Ime, ucenik.Prezime, ucenik.Adresa, ucenik.Razred, ucenik.Jub, new SmerPregled(ucenik.Naziv_smera.Naziv, (int)ucenik.Naziv_smera.maksBrUcenika), ucenik.DatumUpisaSmera));
                }
                foreach (Angazovan angazovan in o.Nastavnici)
                {
                    ZaposleniPregled pom1 = vratiZaposlenig(angazovan.Zaposlen.Jmbg);
                    PredmetPregled pom2 = vratiPredmet(angazovan.Predmet.Ime);
                    pregled.Angazovani.Add(new AngazovanPregled(angazovan.Id, angazovan.DatumOd, angazovan.DatumDo, pom1, pom2));
                }
                foreach (DobijaOcenu dobijaOcenu in o.DobijaUcenikOcenu)
                {
                    pregled.Ocene.Add(new DobijaOcenuPregled(dobijaOcenu.ID, new UcenikPregled(dobijaOcenu.Ucenik.Jmbg, dobijaOcenu.Ucenik.Ime, dobijaOcenu.Ucenik.Prezime, dobijaOcenu.Ucenik.Adresa, dobijaOcenu.Ucenik.Razred, dobijaOcenu.Ucenik.Jub, new SmerPregled(dobijaOcenu.Ucenik.Naziv_smera.Naziv, (int)dobijaOcenu.Ucenik.Naziv_smera.maksBrUcenika), dobijaOcenu.Ucenik.DatumUpisaSmera), null, new PredmetPregled(dobijaOcenu.Predmet.Ime, dobijaOcenu.Predmet.Godina), dobijaOcenu.Datum_upisa_ocene));


                }
                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }

            return pb;
        }

        public static void obrisiPredmet(string ime)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Predmet o = s.Load<Skola.Entiteti.Predmet>(ime);

                s.Delete(o);
                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
        }
        /////////////////////////////////
        ///
        public static List<OpstiPregled> vratiSveOpste()
        {
            List<OpstiPregled> opsti = new List<OpstiPregled>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<Skola.Entiteti.Opsti> sviOpsti = from o in s.Query<Skola.Entiteti.Opsti>()
                                                             select o;
                int i = 0;
                foreach (Skola.Entiteti.Opsti p in sviOpsti)
                {
                    if (p.Ime == null)
                        continue;
                    OpstiPregled pregled = new OpstiPregled();

                    pregled.ImePredmeta = p.Ime;
                    pregled.Godina_studija = p.Godina;
                    pregled.UkupanBrojSmerova = p.UkBrojSmerova;


                    i++;

                    foreach (Skola.Entiteti.Smer smerovi in p.Smerovi)
                    {
                        pregled.Smerovi.Add(new SmerPregled(smerovi.Naziv, (int)smerovi.maksBrUcenika));

                    }
                    foreach (UCenik ucenik in p.Ucenici)
                    {
                        pregled.Ucenici.Add(new UcenikPregled(ucenik.Jmbg, ucenik.Ime, ucenik.Prezime, ucenik.Adresa, ucenik.Razred, ucenik.Jub, new SmerPregled(ucenik.Naziv_smera.Naziv, (int)ucenik.Naziv_smera.maksBrUcenika), ucenik.DatumUpisaSmera));
                    }
                    foreach (Angazovan angazovan in p.Nastavnici)
                    {
                        ZaposleniPregled pom1 = vratiZaposlenig(angazovan.Zaposlen.Jmbg);
                        //PredmetPregled pom2 = vratiPredmet(angazovan.Predmet.Ime);
                        pregled.Angazovani.Add(new AngazovanPregled(angazovan.Id, angazovan.DatumOd, angazovan.DatumDo, pom1, null));
                    }
                    foreach (DobijaOcenu dobijaOcenu in p.DobijaUcenikOcenu)
                    {
                        pregled.Ocene.Add(new DobijaOcenuPregled(dobijaOcenu.ID, new UcenikPregled(dobijaOcenu.Ucenik.Jmbg, dobijaOcenu.Ucenik.Ime, dobijaOcenu.Ucenik.Prezime, dobijaOcenu.Ucenik.Adresa, dobijaOcenu.Ucenik.Razred, dobijaOcenu.Ucenik.Jub, new SmerPregled(dobijaOcenu.Ucenik.Naziv_smera.Naziv, (int)dobijaOcenu.Ucenik.Naziv_smera.maksBrUcenika), dobijaOcenu.Ucenik.DatumUpisaSmera), null, new PredmetPregled(dobijaOcenu.Predmet.Ime, dobijaOcenu.Predmet.Godina), dobijaOcenu.Datum_upisa_ocene));


                    }

                    if (pregled != null)
                        opsti.Add(pregled);

                }

                s.Close();
            }
            catch (Exception ec)
            {

            }

            return opsti;
        }

        public static void dodajOpsti(OpstiPregled p)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Opsti o = new Skola.Entiteti.Opsti();

                o.Ime = p.ImePredmeta;
                o.Godina = p.Godina_studija;
                o.UkBrojSmerova = p.UkupanBrojSmerova;

                s.Save(o);

                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
        }

        public static OpstiPregled azurirajOpsti(OpstiPregled p)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Opsti o = s.Load<Skola.Entiteti.Opsti>(p.ImePredmeta);
                o.Ime = p.ImePredmeta;
                o.Godina = p.Godina_studija;
                o.UkBrojSmerova = p.UkupanBrojSmerova;

                s.Update(o);
                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }

            return p;
        }
        public static bool dodeliSmerOpstem(string Smer, string Predmet)
        {
            try
            {

                ISession s = DataLayer.GetSession();
                Opsti o = s.Load<Skola.Entiteti.Opsti>(Predmet);
                Smer smer = s.Load<Smer>(Smer);
                o.Smerovi.Add(smer);
                smer.Opsti.Add(o);
                s.SaveOrUpdate(o);
                s.SaveOrUpdate(smer);
                s.Flush();
                s.Close();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static OpstiPregled vratiOpsti(string ime)
        {
            OpstiPregled pb = new OpstiPregled();
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Opsti o = s.Load<Skola.Entiteti.Opsti>(ime);
                pb = new OpstiPregled();
                pb.ImePredmeta = o.Ime;
                pb.Godina_studija = o.Godina;
                pb.UkupanBrojSmerova = o.UkBrojSmerova;
                foreach (Skola.Entiteti.Smer smerovi in o.Smerovi)
                {
                    pb.Smerovi.Add(new SmerPregled(smerovi.Naziv, (int)smerovi.maksBrUcenika));
                }
                foreach (UCenik ucenik in o.Ucenici)
                {
                    pb.Ucenici.Add(new UcenikPregled(ucenik.Jmbg, ucenik.Ime, ucenik.Prezime, ucenik.Adresa, ucenik.Razred, ucenik.Jub, new SmerPregled(ucenik.Naziv_smera.Naziv, (int)ucenik.Naziv_smera.maksBrUcenika), ucenik.DatumUpisaSmera));
                }
                foreach (Angazovan angazovan in o.Nastavnici)
                {
                    ZaposleniPregled pom1 = vratiZaposlenig(angazovan.Zaposlen.Jmbg);
                    PredmetPregled pom2 = vratiPredmet(angazovan.Predmet.Ime);
                    pb.Angazovani.Add(new AngazovanPregled(angazovan.Id, angazovan.DatumOd, angazovan.DatumDo, pom1, pom2));
                }
                foreach (DobijaOcenu dobijaOcenu in o.DobijaUcenikOcenu)
                {
                    pb.Ocene.Add(new DobijaOcenuPregled(dobijaOcenu.ID, new UcenikPregled(dobijaOcenu.Ucenik.Jmbg, dobijaOcenu.Ucenik.Ime, dobijaOcenu.Ucenik.Prezime, dobijaOcenu.Ucenik.Adresa, dobijaOcenu.Ucenik.Razred, dobijaOcenu.Ucenik.Jub, new SmerPregled(dobijaOcenu.Ucenik.Naziv_smera.Naziv, (int)dobijaOcenu.Ucenik.Naziv_smera.maksBrUcenika), dobijaOcenu.Ucenik.DatumUpisaSmera), null, new PredmetPregled(dobijaOcenu.Predmet.Ime, dobijaOcenu.Predmet.Godina), dobijaOcenu.Datum_upisa_ocene));


                }

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }

            return pb;
        }

        public static void obrisiOpsti(string ime)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Opsti o = s.Load<Skola.Entiteti.Opsti>(ime);

                s.Delete(o);
                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
        }
        ///////////////////////////////////////////////////////////
        ///
        public static List<UsmereniPregled> vratiSveUsmrene()
        {
            List<UsmereniPregled> usmereni = new List<UsmereniPregled>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<Skola.Entiteti.Usmereni> sviUsmereni = from o in s.Query<Skola.Entiteti.Usmereni>()
                                                                   select o;
                int i = 0;
                foreach (Skola.Entiteti.Usmereni p in sviUsmereni)
                {
                    if (p.Ime == null || p.NazivSmera == null)
                        continue;
                    SmerPregled a = vratiSmer(p.NazivSmera.Naziv);
                    UsmereniPregled pregled = new UsmereniPregled(p.Ime, p.Godina, a); //izmenjeno
                    if (pregled != null)
                        usmereni.Add(pregled);



                    i++;

                }

                s.Close();
            }
            catch (Exception ec)
            {

            }

            return usmereni;
        }
        public static bool DodeliSmerUsmerenom(string Usmereni, string Smer)
        {
            UsmereniPregled p = vratiUsmereni(Usmereni);
            SmerPregled s = vratiSmer(Smer);
            p.Smer = s;
            dodajUsmereni(p);
            return true;
        }

        public static void dodajUsmereni(UsmereniPregled p)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Usmereni o = new Skola.Entiteti.Usmereni();

                o.Ime = p.ImePredmeta;
                o.Godina = p.Godina_studija;
                Smer a = s.Load<Smer>(p.Smer.NazivSmera);
                o.NazivSmera = a;

                s.SaveOrUpdate(o);

                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
        }

        public static UsmereniPregled azurirajUsmereni(UsmereniPregled p)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Usmereni o = s.Load<Skola.Entiteti.Usmereni>(p.ImePredmeta);
                o.Ime = p.ImePredmeta;
                o.Godina = p.Godina_studija;
                Smer a = s.Load<Smer>(p.Smer.NazivSmera);
                o.NazivSmera = a;

                s.Update(o);
                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }

            return p;
        }

        public static UsmereniPregled vratiUsmereni(string ime)
        {
            UsmereniPregled pb = new UsmereniPregled();
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Usmereni o = s.Load<Skola.Entiteti.Usmereni>(ime);
                SmerPregled a = vratiSmer(o.NazivSmera.Naziv);
                pb = new UsmereniPregled();

                pb.ImePredmeta = o.Ime;
                pb.Godina_studija = o.Godina;
                pb.Smer = a;
                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }

            return pb;
        }

        public static void obrisiUsmereni(string ime)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Usmereni o = s.Load<Skola.Entiteti.Usmereni>(ime);

                s.Delete(o);
                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
        }
        /////////////////////////////////////
        ///
        public static List<SmerPregled> vratiSveSmerove()
        {
            List<SmerPregled> smer = new List<SmerPregled>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<Skola.Entiteti.Smer> sviSmerovi = from o in s.Query<Skola.Entiteti.Smer>()
                                                              select o;
                int i = 0;
                foreach (Skola.Entiteti.Smer p in sviSmerovi)
                {
                    if (p.Naziv == null)
                        continue;
                    SmerPregled z = new SmerPregled();
                    z.NazivSmera = p.Naziv;
                    z.MaxBrojUcenika = (int)p.maksBrUcenika;
                    foreach (UCenik a in p.Ucenici)
                    {
                        UcenikPregled b = new UcenikPregled(a.Jmbg, a.Ime, a.Prezime, a.Adresa, a.Razred, a.Jub, null, a.DatumUpisaSmera);
                        z.Ucenici.Add(b);
                    }
                    foreach (Usmereni a in p.Usmereni)
                    {
                        UsmereniPregled b = new UsmereniPregled(a.Ime, a.Godina, null);
                        z.Usmereni.Add(b);
                    }
                    foreach (Opsti a in p.Opsti)
                    {
                        OpstiPregled b = new OpstiPregled(a.Ime, a.Godina, a.UkBrojSmerova);
                        z.Opsti.Add(b);
                    }

                    if (z != null)
                        smer.Add(z);



                    i++;

                }

                s.Close();
            }
            catch (Exception ec)
            {

            }

            return smer;
        }

        public static void dodajSmer(SmerDemo p)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Smer o = new Skola.Entiteti.Smer();

                o.Naziv = p.NazivSmera;
                o.maksBrUcenika = p.MaxBrojUcenika;

                s.SaveOrUpdate(o);

                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
        }
        public static bool dodeliOpstiSmeru(string Predmet, string Smer)
        {
            try
            {

                ISession s = DataLayer.GetSession();
                Opsti o = s.Load<Skola.Entiteti.Opsti>(Predmet);
                Smer smer = s.Load<Smer>(Smer);
                smer.Opsti.Add(o);
                o.Smerovi.Add(smer);
                s.SaveOrUpdate(o);
                s.SaveOrUpdate(smer);
                s.Close();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static void azurirajSmer(SmerAzur p, string Naziv)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Smer o = s.Load<Skola.Entiteti.Smer>(Naziv);

                o.maksBrUcenika = p.MaxBrojUcenika;

                s.Update(o);
                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }


        }

        public static SmerPregled vratiSmer(string ime)
        {
            SmerPregled pb = new SmerPregled();
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Smer o = s.Load<Skola.Entiteti.Smer>(ime);
                pb.NazivSmera = o.Naziv;
                pb.MaxBrojUcenika = (int)o.maksBrUcenika;
                //INICIJALIZUJEM LISTU UCENIKPREGLED SA (tip:List<UCenik>)smer.Ucenici CLANOVE NIZA
                foreach (UCenik ucenik in o.Ucenici)
                {
                    UcenikPregled ucenikPregled = new UcenikPregled(ucenik.Jmbg, ucenik.Ime, ucenik.Prezime, ucenik.Adresa, ucenik.Razred, ucenik.Jub, null, ucenik.DatumUpisaSmera);
                    pb.Ucenici.Add(ucenikPregled);
                }
                foreach (UsmereniPregled a in pb.Usmereni)
                {
                    UsmereniPregled b = new UsmereniPregled(a.ImePredmeta, a.Godina_studija, null);
                    pb.Usmereni.Add(b);
                }
                foreach (OpstiPregled a in pb.Opsti)
                {
                    OpstiPregled b = new OpstiPregled(a.ImePredmeta, a.Godina_studija, a.UkupanBrojSmerova);
                    pb.Opsti.Add(b);
                }



                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }

            return pb;
        }

        public static void obrisiSmer(string ime)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Smer o = s.Load<Skola.Entiteti.Smer>(ime);

                s.Delete(o);
                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////
        ///
        public static List<ZaposleniPregled> vratiSveZaposlene()
        {
            List<ZaposleniPregled> zaposleniKojiPrikazujemo = new List<ZaposleniPregled>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<Skola.Entiteti.Zaposleni> sviZaposleniIzBaze = from o in s.Query<Skola.Entiteti.Zaposleni>()
                                                                           select o;

                foreach (Skola.Entiteti.Zaposleni jedanZaposleniIzBaze in sviZaposleniIzBaze)
                {
                    if (jedanZaposleniIzBaze.Jmbg == null)
                        continue;
                    ZaposleniPregled pregled = new ZaposleniPregled();
                    pregled.Ime = jedanZaposleniIzBaze.Ime;
                    pregled.Prezime = jedanZaposleniIzBaze.Prezime;
                    pregled.Adresa = jedanZaposleniIzBaze.Adresa;
                    pregled.DatumRodjenja = jedanZaposleniIzBaze.DatumRodjenja;
                    pregled.Jmbg = jedanZaposleniIzBaze.Jmbg;

                    if (pregled != null)
                        zaposleniKojiPrikazujemo.Add(pregled);





                }

                s.Close();
            }
            catch (Exception ec)
            {

            }

            return zaposleniKojiPrikazujemo;
        }

        public static void dodajZaposlenig(ZaposleniPregled p)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Zaposleni objekatZaposleni = new Skola.Entiteti.Zaposleni();

                objekatZaposleni.Jmbg = p.Jmbg;
                objekatZaposleni.Ime = p.Ime;
                objekatZaposleni.Prezime = p.Prezime;
                objekatZaposleni.Adresa = p.Adresa;
                objekatZaposleni.DatumRodjenja = p.DatumRodjenja;

                s.SaveOrUpdate(objekatZaposleni);

                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
        }

        public static ZaposleniPregled azurirajZaposlenog(ZaposleniPregled p)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Zaposleni objekatZaposlenizaAzuriranje = s.Load<Skola.Entiteti.Zaposleni>(p.Jmbg);
                objekatZaposlenizaAzuriranje.Jmbg = p.Jmbg;
                objekatZaposlenizaAzuriranje.Ime = p.Ime;
                objekatZaposlenizaAzuriranje.Prezime = p.Prezime;
                objekatZaposlenizaAzuriranje.Adresa = p.Adresa;
                objekatZaposlenizaAzuriranje.DatumRodjenja = p.DatumRodjenja;

                s.Update(objekatZaposlenizaAzuriranje);
                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }

            return p;
        }

        public static ZaposleniPregled vratiZaposlenig(long jmbg)
        {
            ZaposleniPregled jedanZaposleniKojiPrikazujemo = new ZaposleniPregled();
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Zaposleni o = s.Load<Skola.Entiteti.Zaposleni>(jmbg);
                jedanZaposleniKojiPrikazujemo = new ZaposleniPregled();
                jedanZaposleniKojiPrikazujemo.Jmbg = o.Jmbg;
                jedanZaposleniKojiPrikazujemo.Ime = o.Ime;
                jedanZaposleniKojiPrikazujemo.Prezime = o.Prezime;
                jedanZaposleniKojiPrikazujemo.Adresa = o.Adresa;
                jedanZaposleniKojiPrikazujemo.DatumRodjenja = o.DatumRodjenja;



                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }

            return jedanZaposleniKojiPrikazujemo;
        }

        public static void obrisiZaposlenog(long jmbg)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Zaposleni o = s.Load<Skola.Entiteti.Zaposleni>(jmbg);

                s.Delete(o);
                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
        }
        ///////////////////////////////////////////////////////////
        ///
        public static List<NastavnoPregled> vratiSveNastavno()
        {

            List<NastavnoPregled> NastavnoKojaSePrikazuje = new List<NastavnoPregled>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<Skola.Entiteti.Zaposleni> sviNastavno = from o in s.Query<Skola.Entiteti.Zaposleni>()
                                                                    select o;

                foreach (Skola.Entiteti.Zaposleni jedanZaposleni in sviNastavno)
                {
                    if (jedanZaposleni.Jmbg == null)
                        continue;
                    NastavnoPregled jedanOdNastavnoKojaSePrikazuje = new NastavnoPregled();
                    jedanOdNastavnoKojaSePrikazuje.Jmbg = jedanZaposleni.Jmbg;
                    jedanOdNastavnoKojaSePrikazuje.Ime = jedanZaposleni.Ime;
                    jedanOdNastavnoKojaSePrikazuje.Prezime = jedanZaposleni.Prezime;
                    jedanOdNastavnoKojaSePrikazuje.Adresa = jedanZaposleni.Adresa;
                    jedanOdNastavnoKojaSePrikazuje.DatumRodjenja = jedanZaposleni.DatumRodjenja;
                    jedanOdNastavnoKojaSePrikazuje.Angazovan = jedanZaposleni.Angazovan;
                    jedanOdNastavnoKojaSePrikazuje.Norma = jedanZaposleni.Norma;
                    jedanOdNastavnoKojaSePrikazuje.NazivSkole = jedanZaposleni.NazivSkole;
                    jedanOdNastavnoKojaSePrikazuje.BrojCasova = jedanZaposleni.BrojCasova;

                    foreach (Angazovan angazovanObj in jedanZaposleni.Angaovani)
                    {
                        PredmetPregled predmetObj = new PredmetPregled(angazovanObj.Predmet.Ime, angazovanObj.Predmet.Godina);
                        AngazovanPregled angazovan = new AngazovanPregled(angazovanObj.Id, angazovanObj.DatumOd,
                                                                        angazovanObj.DatumDo, null, predmetObj);
                        jedanOdNastavnoKojaSePrikazuje.Angazovani.Add(angazovan);

                    }

                    if (jedanZaposleni != null)
                        NastavnoKojaSePrikazuje.Add(jedanOdNastavnoKojaSePrikazuje);




                }

                s.Close();
            }
            catch (Exception ec)
            {

            }

            return NastavnoKojaSePrikazuje;
        }

        public static void dodajNastavno(NastavnoPregled p)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Zaposleni o = new Skola.Entiteti.Zaposleni();

                o.Jmbg = p.Jmbg;
                o.Ime = p.Ime;
                o.Prezime = p.Prezime;
                o.Adresa = p.Adresa;
                o.DatumRodjenja = p.DatumRodjenja;
                o.Norma = p.Norma;
                o.BrojCasova = p.BrojCasova;
                o.NazivSkole = p.NazivSkole;
                o.TipOsoblja = "NASTAVNO";
                o.Angazovan = p.Angazovan;
                s.SaveOrUpdate(o);

                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
        }

        public static NastavnoPregled azurirajNastavno(NastavnoPregled p)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Zaposleni o = s.Load<Skola.Entiteti.Zaposleni>(p.Jmbg);
                o.Jmbg = p.Jmbg;
                o.Ime = p.Ime;
                o.Prezime = p.Prezime;
                o.Adresa = p.Adresa;
                o.DatumRodjenja = p.DatumRodjenja;
                o.TipOsoblja = "NASTAVNO";
                o.NazivSkole = p.NazivSkole;
                o.Norma = p.Norma;
                o.BrojCasova = p.BrojCasova;
                s.Update(o);
                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }

            return p;
        }

        public static NastavnoPregled vratiNastavno(long jmbg)
        {
            NastavnoPregled pb = new NastavnoPregled();
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Zaposleni o = s.Load<Skola.Entiteti.Zaposleni>(jmbg);

                pb.Jmbg = o.Jmbg;
                pb.Ime = o.Ime;
                pb.Prezime = o.Prezime;
                pb.Adresa = o.Adresa;
                pb.DatumRodjenja = o.DatumRodjenja;
                pb.Angazovan = o.Angazovan;
                pb.Norma = o.Norma;
                pb.NazivSkole = o.NazivSkole;
                pb.BrojCasova = o.BrojCasova;

                foreach (Angazovan angazovanObj in o.Angaovani)
                {
                    PredmetPregled predmetObj = new PredmetPregled(angazovanObj.Predmet.Ime, angazovanObj.Predmet.Godina);
                    AngazovanPregled angazovan = new AngazovanPregled(angazovanObj.Id, angazovanObj.DatumOd,
                                                                    angazovanObj.DatumDo, null, predmetObj);
                    pb.Angazovani.Add(angazovan);

                }

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }

            return pb;
        }

        public static void obrisiNastavno(long jmbg)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Zaposleni o = s.Load<Skola.Entiteti.Zaposleni>(jmbg);

                s.Delete(o);
                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
        }
        ///////////////////////////////////////////////////////////////////
        ///
        public static List<NeNastavnoPregled> vratiSveNeNastavno()
        {
            List<NeNastavnoPregled> nenastavno = new List<NeNastavnoPregled>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<Skola.Entiteti.Zaposleni> sviNastavno = from o in s.Query<Skola.Entiteti.Zaposleni>()
                                                                    select o;
                int i = 0;
                foreach (Skola.Entiteti.Zaposleni p in sviNastavno)
                {
                    if (p.Jmbg == null)
                        continue;
                    NeNastavnoPregled pregled = new NeNastavnoPregled();

                    pregled.Jmbg = p.Jmbg;
                    pregled.Ime = p.Ime;
                    pregled.Prezime = p.Prezime;
                    pregled.Adresa = p.Adresa;
                    pregled.DatumRodjenja = p.DatumRodjenja;
                    pregled.SektorRada = p.SektorRada;
                    pregled.StrucnaSprema = p.StrucnaSprema;
                    if (pregled != null)
                        nenastavno.Add(pregled);



                    i++;

                }

                s.Close();
            }
            catch (Exception ec)
            {

            }

            return nenastavno;
        }

        public static void dodajNeNastavno(NeNastavnoPregled p)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Zaposleni o = new Skola.Entiteti.Zaposleni();

                o.Jmbg = p.Jmbg;
                o.Ime = p.Ime;
                o.Prezime = p.Prezime;
                o.Adresa = p.Adresa;
                o.DatumRodjenja = p.DatumRodjenja;
                o.StrucnaSprema = p.StrucnaSprema;
                o.SektorRada = p.SektorRada;
                o.TipOsoblja = "NENASTAVNO";
                s.SaveOrUpdate(o);

                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
        }

        public static NeNastavnoPregled azurirajNeNastavno(NeNastavnoPregled p)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Zaposleni o = s.Load<Skola.Entiteti.Zaposleni>(p.Jmbg);
                o.Jmbg = p.Jmbg;
                o.Ime = p.Ime;
                o.Prezime = p.Prezime;
                o.Adresa = p.Adresa;
                o.DatumRodjenja = p.DatumRodjenja;
                o.TipOsoblja = "NENASTAVNO";
                o.SektorRada = p.SektorRada;
                o.StrucnaSprema = p.StrucnaSprema;
                s.Update(o);
                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }

            return p;
        }

        public static NeNastavnoPregled vratiNeNastavno(long jmbg)
        {
            NeNastavnoPregled pb = new NeNastavnoPregled();
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Zaposleni o = s.Load<Skola.Entiteti.Zaposleni>(jmbg);
                pb = new NeNastavnoPregled(o.Jmbg, o.Ime, o.Prezime, o.Adresa, o.DatumRodjenja, o.SektorRada, o.StrucnaSprema);

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }

            return pb;
        }

        public static void obrisiNeNastavno(long jmbg)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Zaposleni o = s.Load<Skola.Entiteti.Zaposleni>(jmbg);

                s.Delete(o);
                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
        }
        /// <summary>
        /// ////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        public static List<OcenaPregled> vratiSveOcene()
        {
            List<OcenaPregled> ocene = new List<OcenaPregled>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<Skola.Entiteti.Ocena> sveOcene = from o in s.Query<Skola.Entiteti.Ocena>()
                                                             select o;
                int i = 0;
                foreach (Skola.Entiteti.Ocena p in sveOcene)
                {
                    if (p.ID == null)
                        continue;
                    OcenaPregled pregled = new OcenaPregled();
                    pregled.ID = p.ID;
                    pregled.Vrednost = (int)p.Vrednost;
                    pregled.TekstualniOpis = p.tekstOpis;
                    foreach (DobijaOcenu dobija in p.DobijaUcenikIzPredmeta)
                    {
                        UcenikPregled ucenik = new UcenikPregled(dobija.Ucenik.Jmbg, dobija.Ucenik.Ime, dobija.Ucenik.Prezime, dobija.Ucenik.Adresa, dobija.Ucenik.Razred, dobija.Ucenik.Jub, new SmerPregled(dobija.Ucenik.Naziv_smera.Naziv, (int)dobija.Ucenik.Naziv_smera.maksBrUcenika), dobija.Ucenik.DatumUpisaSmera);
                        pregled.Ocena.Add(new DobijaOcenuPregled(dobija.ID, ucenik, new OcenaPregled(dobija.Ocena.ID, (int)dobija.Ocena.Vrednost, dobija.Ocena.tekstOpis), new PredmetPregled(dobija.Predmet.Ime, dobija.Predmet.Godina), dobija.Datum_upisa_ocene));
                    }

                    if (pregled != null)
                        ocene.Add(pregled);



                    i++;

                }

                s.Close();
            }
            catch (Exception ec)
            {

            }

            return ocene;
        }

        public static void dodajOcenu(OcenaPregled p)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Ocena o = new Skola.Entiteti.Ocena();

                o.Vrednost = p.Vrednost;
                o.tekstOpis = p.TekstualniOpis;

                s.SaveOrUpdate(o);

                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
        }

        public static OcenaPregled azurirajOcenu(OcenaPregled p)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Ocena o = s.Load<Skola.Entiteti.Ocena>(p.ID);
                o.Vrednost = p.Vrednost;
                o.tekstOpis = p.TekstualniOpis;

                s.Update(o);
                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }

            return p;
        }

        public static OcenaPregled vratiOcenu(int id)
        {
            OcenaPregled pb = new OcenaPregled();
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Ocena o = s.Load<Skola.Entiteti.Ocena>(id);
                pb = new OcenaPregled();

                pb.ID = o.ID;
                pb.Vrednost = (int)o.Vrednost;
                pb.TekstualniOpis = o.tekstOpis;
                foreach (DobijaOcenu dobija in o.DobijaUcenikIzPredmeta)
                {
                    UcenikPregled ucenik = new UcenikPregled(dobija.Ucenik.Jmbg, dobija.Ucenik.Ime, dobija.Ucenik.Prezime, dobija.Ucenik.Adresa, dobija.Ucenik.Razred, dobija.Ucenik.Jub, new SmerPregled(dobija.Ucenik.Naziv_smera.Naziv, (int)dobija.Ucenik.Naziv_smera.maksBrUcenika), dobija.Ucenik.DatumUpisaSmera);
                    pb.Ocena.Add(new DobijaOcenuPregled(dobija.ID, ucenik, new OcenaPregled(dobija.Ocena.ID, (int)dobija.Ocena.Vrednost, dobija.Ocena.tekstOpis), new PredmetPregled(dobija.Predmet.Ime, dobija.Predmet.Godina), dobija.Datum_upisa_ocene));
                }

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }

            return pb;
        }

        public static void obrisiOcenu(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Ocena o = s.Load<Skola.Entiteti.Ocena>(id);

                s.Delete(o);
                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
        }
        ///////////////////////////////////////
        ///
        public static List<DobijaOcenuPregled> vratiSveDobijeneOcene()
        {
            List<DobijaOcenuPregled> dobija = new List<DobijaOcenuPregled>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<Skola.Entiteti.DobijaOcenu> sviDobija = from o in s.Query<Skola.Entiteti.DobijaOcenu>()
                                                                    select o;
                int i = 0;
                foreach (Skola.Entiteti.DobijaOcenu p in sviDobija)
                {
                    if (p.ID == null || p.Ucenik == null || p.Predmet == null || p.Ocena == null)
                        continue;
                    SmerPregled zmer = vratiSmer(p.Ucenik.Naziv_smera.Naziv);

                    UcenikPregled ucenikPregled = new UcenikPregled(p.Ucenik.Jmbg, p.Ucenik.Ime, p.Ucenik.Prezime, p.Ucenik.Adresa, p.Ucenik.Razred, p.Ucenik.Jub, zmer, p.Ucenik.DatumUpisaSmera);
                    OcenaPregled ocenaPregled = new OcenaPregled(p.Ocena.ID, (int)p.Ocena.Vrednost, p.Ocena.tekstOpis);
                    PredmetPregled predmetPregled = new PredmetPregled(p.Predmet.Ime, p.Predmet.Godina);
                    DobijaOcenuPregled pregled = new DobijaOcenuPregled(p.ID, ucenikPregled, ocenaPregled, predmetPregled, p.Datum_upisa_ocene);

                    if (pregled != null)
                        dobija.Add(pregled);



                    i++;

                }

                s.Close();
            }
            catch (Exception ec)
            {

            }

            return dobija;
        }

        public static void dodajDobija(DobijaOcenuDemo p, long Jmbg, string Predmet, int Ocena)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.DobijaOcenu o = new Skola.Entiteti.DobijaOcenu();

                o.ID = p.ID;

                o.Predmet = s.Load<Predmet>(Predmet);
                o.Ocena = s.Load<Ocena>(Ocena);
                o.Ucenik = s.Load<UCenik>(Jmbg);
                o.Datum_upisa_ocene = p.DatumUpisaOcene;

                s.Save(o);

                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
        }

        public static void azurirajDobija(DobijaOcenuAzur p, long Jmbg, string Predmet, int IdOcene, int ID)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.DobijaOcenu o = s.Load<Skola.Entiteti.DobijaOcenu>(ID);

                o.Predmet = s.Load<Predmet>(Predmet);
                o.Ocena = s.Load<Ocena>(IdOcene);
                o.Ucenik = s.Load<UCenik>(Jmbg);
                o.Datum_upisa_ocene = p.DatumUpisaOcene;

                s.Update(o);
                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }


        }

        public static DobijaOcenuPregled vratiDobija(int id)
        {
            DobijaOcenuPregled pb = new DobijaOcenuPregled();
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.DobijaOcenu o = s.Load<Skola.Entiteti.DobijaOcenu>(id);
                SmerPregled zmer = vratiSmer(o.Ucenik.Naziv_smera.Naziv);
                UcenikPregled ucenikPregled = new UcenikPregled(o.Ucenik.Jmbg, o.Ucenik.Ime, o.Ucenik.Prezime, o.Ucenik.Adresa, o.Ucenik.Razred, o.Ucenik.Jub, zmer, o.Ucenik.DatumUpisaSmera);
                OcenaPregled ocenaPregled = new OcenaPregled(o.Ocena.ID, (int)o.Ocena.Vrednost, o.Ocena.tekstOpis);
                PredmetPregled predmetPregled = new PredmetPregled(o.Predmet.Ime, o.Predmet.Godina);
                DobijaOcenuPregled pregled = new DobijaOcenuPregled(o.ID, ucenikPregled, ocenaPregled, predmetPregled, o.Datum_upisa_ocene);

                pb = new DobijaOcenuPregled(o.ID, ucenikPregled, ocenaPregled, predmetPregled, o.Datum_upisa_ocene);

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }

            return pb;
        }

        public static void obrisiDobija(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.DobijaOcenu o = s.Load<Skola.Entiteti.DobijaOcenu>(id);

                s.Delete(o);
                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////
        ///
        public static List<AngazovanPregled> vratiSveAngazovane()
        {
            List<AngazovanPregled> angazovani = new List<AngazovanPregled>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<Skola.Entiteti.Angazovan> sviAngazovani = from o in s.Query<Skola.Entiteti.Angazovan>()
                                                                      select o;
                int i = 0;
                foreach (Skola.Entiteti.Angazovan p in sviAngazovani)
                {
                    if (p.Id == null || p.Zaposlen == null || p.Predmet == null)
                        continue;
                    AngazovanPregled pregled = new AngazovanPregled(p.Id, p.DatumOd, p.DatumDo, new ZaposleniPregled(p.Zaposlen.Jmbg, p.Zaposlen.Ime, p.Zaposlen.Prezime, p.Zaposlen.Adresa, p.Zaposlen.DatumRodjenja), new PredmetPregled(p.Predmet.Ime, p.Predmet.Godina));
                    if (pregled != null)
                        angazovani.Add(pregled);



                    i++;

                }

                s.Close();
            }
            catch (Exception ec)
            {

            }

            return angazovani;
        }

        public static void dodajAngazovan(AngazovanDemo p, long Jmbg, string Naziv)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Angazovan o = new Skola.Entiteti.Angazovan();

                o.Id = p.ID;
                Predmet predmetPom = s.Load<Predmet>(Naziv);
                o.Predmet = predmetPom;
                Zaposleni zaposleniPom = s.Load<Zaposleni>(Jmbg);
                o.Zaposlen = zaposleniPom;
                o.DatumOd = p.DatumOd;
                o.DatumDo = p.DatumDo;

                s.Save(o);

                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
        }

        public static void azurirajAngazovan(AngazovanAzur p, long Jmbg, string Predmet, int ID)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Angazovan o = s.Load<Skola.Entiteti.Angazovan>(ID);
                Predmet predmet = s.Load<Predmet>(Predmet);

                o.Predmet = predmet;
                Zaposleni zaposleniPom = s.Load<Zaposleni>(Jmbg);
                o.Zaposlen = zaposleniPom;
                o.DatumDo = p.DatumDo;
                o.DatumOd = p.DatumOd;

                s.Update(o);
                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }


        }

        public static AngazovanPregled vratiAngazovan(int id)
        {
            AngazovanPregled pb = new AngazovanPregled();
            try
            {
                ISession s = DataLayer.GetSession();


                Skola.Entiteti.Angazovan o = s.Load<Skola.Entiteti.Angazovan>(id);
                pb = new AngazovanPregled((int)o.Id, o.DatumOd, o.DatumDo, new ZaposleniPregled(o.Zaposlen.Jmbg, o.Zaposlen.Ime, o.Zaposlen.Prezime, o.Zaposlen.Adresa, o.Zaposlen.DatumRodjenja), new PredmetPregled(o.Predmet.Ime, o.Predmet.Godina));

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }

            return pb;
        }

        public static void obrisiAngazovan(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.Angazovan o = s.Load<Skola.Entiteti.Angazovan>(id);

                s.Delete(o);
                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
        }
        /////////////////////////////
        ///
        public static List<BrojTelefonaPregled> vratiSveBrojeve()
        {
            List<BrojTelefonaPregled> brojevi = new List<BrojTelefonaPregled>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<Skola.Entiteti.BrojTelefona> sviBrojevi = from o in s.Query<Skola.Entiteti.BrojTelefona>()
                                                                      select o;
                int i = 0;
                foreach (Skola.Entiteti.BrojTelefona p in sviBrojevi)
                {

                    UcenikPregled ucenik = new UcenikPregled(p.Roditelj.Ucenik.Jmbg, p.Roditelj.Ucenik.Ime, p.Roditelj.Ucenik.Prezime, p.Roditelj.Ucenik.Adresa, p.Roditelj.Ucenik.Razred, p.Roditelj.Ucenik.Jub, new SmerPregled(p.Roditelj.Ucenik.Naziv_smera.Naziv, (int)p.Roditelj.Ucenik.Naziv_smera.maksBrUcenika), p.Roditelj.Ucenik.DatumUpisaSmera);
                    BrojTelefonaPregled pregled = new BrojTelefonaPregled(p.vrednost, new RoditeljPregled((int)p.Roditelj.Id, p.Roditelj.Ime, p.Roditelj.Prezime, p.Roditelj.Clan_veca, ucenik));
                    if (pregled != null)
                        brojevi.Add(pregled);



                    i++;

                }

                s.Close();
            }
            catch (Exception ec)
            {

            }

            return brojevi;
        }

        public static void dodajBroj(int broj, int roditelj)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.BrojTelefona o = new Skola.Entiteti.BrojTelefona();

                o.vrednost = broj;
                o.Roditelj = s.Load<Roditelj>(roditelj);

                s.Save(o);

                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
        }

        public static void azurirajBroj(int vrednost, int roditelj)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.BrojTelefona o = s.Load<Skola.Entiteti.BrojTelefona>(vrednost);


                o.Roditelj = s.Load<Roditelj>(roditelj);

                s.Update(o);
                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }


        }

        public static BrojTelefonaPregled vratiBroj(int broj)
        {
            BrojTelefonaPregled pb = new BrojTelefonaPregled();
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.BrojTelefona o = s.Load<Skola.Entiteti.BrojTelefona>(broj);
                UcenikPregled ucenik = new UcenikPregled(o.Roditelj.Ucenik.Jmbg, o.Roditelj.Ucenik.Ime, o.Roditelj.Ucenik.Prezime, o.Roditelj.Ucenik.Adresa, o.Roditelj.Ucenik.Razred, o.Roditelj.Ucenik.Jub, new SmerPregled(o.Roditelj.Ucenik.Naziv_smera.Naziv, (int)o.Roditelj.Ucenik.Naziv_smera.maksBrUcenika), o.Roditelj.Ucenik.DatumUpisaSmera);

                pb.Roditelj = new RoditeljPregled((int)o.Roditelj.Id, o.Roditelj.Ime, o.Roditelj.Prezime, o.Roditelj.Clan_veca, ucenik);
                pb.BrojTelefona = o.vrednost;
                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }

            return pb;
        }

        public static void obrisiBrojTelefona(int broj)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Skola.Entiteti.BrojTelefona o = s.Load<Skola.Entiteti.BrojTelefona>(broj);

                s.Delete(o);
                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
        }
    }

}
