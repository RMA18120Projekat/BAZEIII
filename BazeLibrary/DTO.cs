using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

using Skola.Entiteti;

namespace Skola
{
    public class UcenikAzur
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int JUB { get; set; }
        public int Razred { get; set; }
        // public Skola.Entiteti.Smer NazivSmera;
        public string DatumUpisa { get; set; }
        public string Adresa { get; set; }
        /// <summary>
        /// //////////////////////////////
        /// </summary>
        /// 

        public UcenikAzur()
        {


        }
        public UcenikAzur(string ime, string prezime, string adresa, int razred, int jub, string DatumUpisa)
        {
            this.Ime = ime;
            this.Razred = razred;
            this.Prezime = prezime;

            this.DatumUpisa = DatumUpisa;
            this.JUB = jub;
            this.Adresa = adresa;
        }

    }
    public class UcenikDemo
    {
        public long Jmbg { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int JUB { get; set; }
        public int Razred { get; set; }
        // public Skola.Entiteti.Smer NazivSmera;
        public string DatumUpisa { get; set; }
        public string Adresa { get; set; }
        /// <summary>
        /// //////////////////////////////
        /// </summary>
        /// 
        
        public UcenikDemo()
        {

            
        }
        public UcenikDemo(long jmbg, string ime, string prezime,string adresa, int razred, int jub, string DatumUpisa)
        {
            this.Jmbg = jmbg;
            this.Ime = ime;
            this.Razred = razred;
            this.Prezime = prezime;
            
            this.DatumUpisa = DatumUpisa;
            this.JUB = jub;
            this.Adresa = adresa;
        }

    }


    public class UcenikPregled
    {
        public long Jmbg { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int JUB { get; set; }
        public string Adresa { get; set; }
        public int Razred { get; set; }
        // public Skola.Entiteti.Smer NazivSmera;
        public string DatumUpisa { get; set; }
        /// <summary>
        /// //////////////////////////////
        /// </summary>
        /// 
        public SmerPregled NazivSmera { get; set; }
        

        public virtual IList<RoditeljPregled> Roditelji { get; set; }
        public virtual IList<PredmetPregled> Predmeti { get; set; }
        public virtual IList<DobijaOcenuPregled> Ocene { get; set; }


        public UcenikPregled()
        {

            this.Roditelji = new List<RoditeljPregled>();
            this.Predmeti = new List<PredmetPregled>();
            this.Ocene = new List<DobijaOcenuPregled>();

        }
        public UcenikPregled(long jmbg, string ime, string prezime,string adresa, int razred,int jub, SmerPregled nazivSmera, string DatumUpisa)
        {
            this.Jmbg = jmbg;
            this.Ime = ime;
            this.Razred = razred;
            this.Prezime = prezime;
            this.NazivSmera = nazivSmera;
            this.DatumUpisa = DatumUpisa;
            this.JUB = jub;
            this.Adresa = adresa;
        }

    }
    public class RoditeljDemo
    {
        public int ID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string ClanVeca { get; set; }

        

        public RoditeljDemo()
        {

        
        }

        public RoditeljDemo(int id, string ime, string prezime, string ClanVeca)
        {
            this.ID = id;
            this.Ime = ime;
            this.Prezime = prezime;
            this.ClanVeca = ClanVeca;
            
        }

    }
    public class RoditeljAzur
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string ClanVeca { get; set; }



        public RoditeljAzur()
        {


        }

        public RoditeljAzur(string ime, string prezime, string ClanVeca)
        {
            this.Ime = ime;
            this.Prezime = prezime;
            this.ClanVeca = ClanVeca;

        }

    }



    public class RoditeljPregled
    {
        public int? ID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string ClanVeca { get; set; }
        
        public UcenikPregled Ucenik { get; set; }
        public IList<BrojTelefonaPregled> Brojevi { get; set; }


        public RoditeljPregled()
        {

            this.Brojevi = new List<BrojTelefonaPregled>();
        }
       
        public RoditeljPregled(int id, string ime, string prezime, string ClanVeca,UcenikPregled JMBG)
        {
            this.ID = id;
            this.Ime = ime;
            this.Prezime = prezime;
            this.ClanVeca = ClanVeca;
            this.Ucenik = JMBG;

        }

    }

    
        public class PredmetPregled
        {
            public string ImePredmeta { get; set; }

        public int Godina_studija { get; set; }
        public virtual IList<UcenikPregled> Ucenici { get; set; }
        public virtual IList<AngazovanPregled> Angazovani { get; set; }
        public virtual IList<DobijaOcenuPregled> Ocene { get; set; }

        public PredmetPregled()
            {
            Ucenici = new List<UcenikPregled>();
            Angazovani = new List<AngazovanPregled>();
            Ocene = new List<DobijaOcenuPregled>();
        }

            public PredmetPregled(
                string imePredemta,
                int _godStud
            
            )
            {
                this.Godina_studija = _godStud;
                this.ImePredmeta= imePredemta;
            
            }
    }
    public class PredmetDemo
    {
        public string ImePredmeta { get; set; }

        public int Godina_studija { get; set; }
        
        public PredmetDemo()
        {
       
        }

        public PredmetDemo(
            string imePredemta,
            int _godStud

        )
        {
            this.Godina_studija = _godStud;
            this.ImePredmeta = imePredemta;

        }
    }
    public class PredmetAzur
    {
        
        public int Godina_studija { get; set; }

        public PredmetAzur()
        {

        }

        public PredmetAzur(
            int _godStud

        )
        {
            this.Godina_studija = _godStud;
            
        }
    }

    //////////////////
    /// <summary>

    /// </summary>
    public class UsmereniDemo : PredmetDemo
    {

        //public Skola.Entiteti.Smer Smer;
        

    public UsmereniDemo()
    { }

    public UsmereniDemo(string imePredemta,
        int _godStud) : base(imePredemta, _godStud)
    {
       
    }
}
    public class UsmereniAzur : PredmetAzur
    {

        //public Skola.Entiteti.Smer Smer;
        
        public UsmereniAzur()
        { }

        public UsmereniAzur(
            int _godStud) : base(_godStud)
        {
            
        }
    }
    public class UsmereniPregled : PredmetPregled
    {

        //public Skola.Entiteti.Smer Smer;
        public SmerPregled Smer { get; set; }


        public UsmereniPregled()
        { }

        public UsmereniPregled(string imePredemta,
            int _godStud,
             SmerPregled smer) : base(imePredemta, _godStud)
        {
            this.Smer = smer;
        }
    }

     
    public class OpstiPregled : PredmetPregled
    {
        public int UkupanBrojSmerova { get; set; }
        public virtual IList<Skola.SmerPregled> Smerovi { get; set; }


        public OpstiPregled()
        {
            Smerovi = new List<Skola.SmerPregled>();

        }

        public OpstiPregled(string imePredemta,
            int _godStud,
            int _ukupanBrojSmerova) : base(imePredemta, _godStud)
        {
            this.UkupanBrojSmerova = _ukupanBrojSmerova;
        }
    }
    public class OpstiDemo : PredmetDemo
    {
        public int UkupanBrojSmerova { get; set; }
        

        public OpstiDemo()
        {
            
        }

        public OpstiDemo(string imePredemta,
            int _godStud,
            int _ukupanBrojSmerova) : base(imePredemta, _godStud)
        {
            this.UkupanBrojSmerova = _ukupanBrojSmerova;
        }
    }
    public class OpstiAzur : PredmetAzur
    {
        public int UkupanBrojSmerova { get; set; }
        

        public OpstiAzur()
        {
            
        }

        public OpstiAzur(
            int _godStud,
            int _ukupanBrojSmerova) : base(_godStud)
        {
            this.UkupanBrojSmerova = _ukupanBrojSmerova;
        }
    }


    public class SmerPregled
    {

        public string NazivSmera { get; set; }
        
        public int MaxBrojUcenika { get; set; }
        public virtual IList<UcenikPregled> Ucenici { get; set; }
        public virtual IList<UsmereniPregled> Usmereni { get; set; }
        public virtual IList<OpstiPregled> Opsti { get; set; }


        public SmerPregled()
        {
            Ucenici = new List<UcenikPregled>();
            Usmereni = new List<UsmereniPregled>();
            Opsti = new List<OpstiPregled>();

        }

        public SmerPregled(
        string _nazivSmera,
        int _maxBrojUcenika)
        {
            this.NazivSmera = _nazivSmera;
            this.MaxBrojUcenika = _maxBrojUcenika;

        }
    }
    public class SmerDemo
    {

        public string NazivSmera { get; set; }

        public int MaxBrojUcenika { get; set; }
       

        public SmerDemo()
        {
           

        }

        public SmerDemo(
        string _nazivSmera,
        int _maxBrojUcenika)
        {
            this.NazivSmera = _nazivSmera;
            this.MaxBrojUcenika = _maxBrojUcenika;

        }
    }
    public class SmerAzur
    {

        
        public int MaxBrojUcenika { get; set; }


        public SmerAzur()
        {


        }

        public SmerAzur(
        int _maxBrojUcenika)
        {
            this.MaxBrojUcenika = _maxBrojUcenika;

        }
    }


    /////////////////////////////////////
    ///
    public class ZaposleniPregled
    {

        public long Jmbg { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Adresa { get; set; }
        public string DatumRodjenja { get; set; }

        /* public string SektorRada;
         public string StrucnaSprema;
         public string Norma;
         public string Angazovan;
         public int BrojCasova;
         public string NazivSkole;*/




        public ZaposleniPregled()
        { }

        public ZaposleniPregled(
         long Jmbg,
         string Ime,
         string  Prezime,
         string Adresa,
         string DatumRodjenja
    
        )
        {
            this.Jmbg = Jmbg;
            this.Ime = Ime;
            this.Prezime = Prezime;
            this.Adresa = Adresa;
            this.DatumRodjenja = DatumRodjenja;
        }
    }
    public class ZaposleniDemo
    {

        public long Jmbg { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Adresa { get; set; }
        public string DatumRodjenja { get; set; }

        /* public string SektorRada;
         public string StrucnaSprema;
         public string Norma;
         public string Angazovan;
         public int BrojCasova;
         public string NazivSkole;*/




        public ZaposleniDemo()
        { }

        public ZaposleniDemo(
         long Jmbg,
         string Ime,
         string Prezime,
         string Adresa,
         string DatumRodjenja

        )
        {
            this.Jmbg = Jmbg;
            this.Ime = Ime;
            this.Prezime = Prezime;
            this.Adresa = Adresa;
            this.DatumRodjenja = DatumRodjenja;
        }
    }
    public class ZaposleniAzur
    {

       
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Adresa { get; set; }
        public string DatumRodjenja { get; set; }

        /* public string SektorRada;
         public string StrucnaSprema;
         public string Norma;
         public string Angazovan;
         public int BrojCasova;
         public string NazivSkole;*/




        public ZaposleniAzur()
        { }

        public ZaposleniAzur(
         
         string Ime,
         string Prezime,
         string Adresa,
         string DatumRodjenja

        )
        {
           
            this.Ime = Ime;
            this.Prezime = Prezime;
            this.Adresa = Adresa;
            this.DatumRodjenja = DatumRodjenja;
        }
    }



    public class NastavnoPregled:ZaposleniPregled
    {

        public string Norma { get; set; }
        public string Angazovan { get; set; }
        public int? BrojCasova { get; set; }
        public string NazivSkole { get; set; }
        public IList<AngazovanPregled> Angazovani { get; set; }





        public NastavnoPregled()
        {
            Angazovani = new List<AngazovanPregled>();

        }

        public NastavnoPregled(
        long Jmbg,
        string Ime,
        string Prezime,
        string Adresa,
        string DatumRodjenja,
        string Norma,
        string Angazovan,
        int? BrojCasova,
        string NazivSkole


        )
            :base(Jmbg,Ime,Prezime,Adresa,DatumRodjenja)
        {
            this.Norma = Norma;
            this.Angazovan = Angazovan;
            this.BrojCasova = BrojCasova;
            this.NazivSkole = NazivSkole;
        }
    }

    public class NastavnoDemo: ZaposleniDemo
    {

        public string Norma { get; set; }
        public string Angazovan { get; set; }
        public int? BrojCasova { get; set; }
        public string NazivSkole { get; set; }
      





        public NastavnoDemo()
        {
          

        }

        public NastavnoDemo(
        long Jmbg,
        string Ime,
        string Prezime,
        string Adresa,
        string DatumRodjenja,
        string Norma,
        string Angazovan,
        int? BrojCasova,
        string NazivSkole


        )
            : base(Jmbg, Ime, Prezime, Adresa, DatumRodjenja)
        {
            this.Norma = Norma;
            this.Angazovan = Angazovan;
            this.BrojCasova = BrojCasova;
            this.NazivSkole = NazivSkole;
        }
    }


    public class NastavnoAzuriraj: ZaposleniAzur
    {

        public string Norma { get; set; }
        public string Angazovan { get; set; }
        public int? BrojCasova { get; set; }
        public string NazivSkole { get; set; }






        public NastavnoAzuriraj()
        {


        }

        public NastavnoAzuriraj(
       
        string Ime,
        string Prezime,
        string Adresa,
        string DatumRodjenja,
        string Norma,
        string Angazovan,
        int? BrojCasova,
        string NazivSkole


        )
            : base( Ime, Prezime, Adresa, DatumRodjenja)
        {
            this.Norma = Norma;
            this.Angazovan = Angazovan;
            this.BrojCasova = BrojCasova;
            this.NazivSkole = NazivSkole;
        }
    }





    public class NeNastavnoPregled:ZaposleniPregled
    {

        public string SektorRada { get; set; }
        public string StrucnaSprema { get; set; }


        public NeNastavnoPregled()
        { }

        public NeNastavnoPregled(
        long Jmbg,
        string Ime,
        string Prezime,
        string Adresa,
        string DatumRodjenja,
        string SektorRada,
        string StrucnaSprema
        )
            :base(Jmbg,Ime,Prezime,Adresa,DatumRodjenja)
        {
            this.SektorRada = SektorRada;
            this.StrucnaSprema = StrucnaSprema;
       
        }
    }
    public class NeNastavnoDemo : ZaposleniDemo
    {

        public string SektorRada { get; set; }
        public string StrucnaSprema { get; set; }


        public NeNastavnoDemo()
        { }

        public NeNastavnoDemo(
        long Jmbg,
        string Ime,
        string Prezime,
        string Adresa,
        string DatumRodjenja,
        string SektorRada,
        string StrucnaSprema
        )
            : base(Jmbg, Ime, Prezime, Adresa, DatumRodjenja)
        {
            this.SektorRada = SektorRada;
            this.StrucnaSprema = StrucnaSprema;

        }
    }
    public class NeNastavnoAzur : ZaposleniAzur
    {

        public string SektorRada { get; set; }
        public string StrucnaSprema { get; set; }


        public NeNastavnoAzur()
        { }

        public NeNastavnoAzur(
        
        string Ime,
        string Prezime,
        string Adresa,
        string DatumRodjenja,
        string SektorRada,
        string StrucnaSprema
        )
            : base(Ime, Prezime, Adresa, DatumRodjenja)
        {
            this.SektorRada = SektorRada;
            this.StrucnaSprema = StrucnaSprema;

        }
    }


    /////////////////////////////////////////////////
    ///
    public class OcenaPregled
    {
        public int? ID { get; set; }
        public int Vrednost { get; set; }
        public string TekstualniOpis { get; set; }
        public virtual IList<DobijaOcenuPregled> Ocena { get; set; }


        public OcenaPregled()
        {
            Ocena = new List<DobijaOcenuPregled>();

        }

        public OcenaPregled(
        int? _ID,
        int _vrednost,
        string tekstualniOpis)
        {
            this.ID = _ID;
            this.Vrednost = _vrednost;
            this.TekstualniOpis = tekstualniOpis;

        }
    }
    public class OcenaDemo
    {
        public int? ID { get; set; }
        public int Vrednost { get; set; }
        public string TekstualniOpis { get; set; }
        

        public OcenaDemo()
        {
            
        }

        public OcenaDemo(
        int? _ID,
        int _vrednost,
        string tekstualniOpis)
        {
            this.ID = _ID;
            this.Vrednost = _vrednost;
            this.TekstualniOpis = tekstualniOpis;

        }
    }
    public class OcenaAzur
    {
        public int Vrednost { get; set; }
        public string TekstualniOpis { get; set; }
        
        public OcenaAzur()
        {
            
        }

        public OcenaAzur(
        int? _ID,
        int _vrednost,
        string tekstualniOpis)
        {
            this.Vrednost = _vrednost;
            this.TekstualniOpis = tekstualniOpis;

        }
    }



    /// <summary>
    /// ////////////////////////////////
    /// </summary>
    public class AngazovanPregled
    {

        public int? ID { get; set; }
        //public Predmet nazivPredmeta;
        //public Zaposleni nastavnik;
        public string DatumOd { get; set; }
        public string DatumDo { get; set; }
    public ZaposleniPregled Zaposleni { get; set; }
    public PredmetPregled NazivPredmeta { get; set; }





    public AngazovanPregled()
        { }

        public AngazovanPregled(
        int? _ID,
        string _datumOd,
        string _datumDo,
        ZaposleniPregled _zaposleni,
        PredmetPregled naziv_predmeta
        
        )
        {
           
            this.NazivPredmeta = naziv_predmeta;
            this.ID = _ID;
            this.DatumDo = _datumDo;
            this.DatumOd = _datumOd;
            this.Zaposleni = _zaposleni;
            
        }


    }
    public class AngazovanDemo
    {

        public int? ID { get; set; }
        //public Predmet nazivPredmeta;
        //public Zaposleni nastavnik;
        public string DatumOd { get; set; }
        public string DatumDo { get; set; }
        



        public AngazovanDemo()
        { }

        public AngazovanDemo(
        int? _ID,
        string _datumOd,
        string _datumDo
        
        )
        {

            this.ID = _ID;
            this.DatumDo = _datumDo;
            this.DatumOd = _datumOd;
            
        }


    }
    public class AngazovanAzur
    {

        //public Predmet nazivPredmeta;
        //public Zaposleni nastavnik;
        public string DatumOd { get; set; }
        public string DatumDo { get; set; }




        public AngazovanAzur()
        { }

        public AngazovanAzur(
        
        string _datumOd,
        string _datumDo

        )
        {

            this.DatumDo = _datumDo;
            this.DatumOd = _datumOd;

        }


    }

    public class DobijaOcenuPregled
    {
        public int? ID { get; set; }
        public string DatumUpisaOcene { get; set; }
    public UcenikPregled JMBG { get; set; }
    public OcenaPregled ID_ocene { get; set; }
    public PredmetPregled NazivPredmeta { get; set; }



    public DobijaOcenuPregled()
        { }

       public  DobijaOcenuPregled(
        int? _ID,
        UcenikPregled _JMBG,
        OcenaPregled _ID_ocene,
        PredmetPregled _nazivPredmeta,
        string _datumUpisaOcene
       )
        {
            this.ID = _ID;
            this.JMBG = _JMBG;
            this.ID_ocene = _ID_ocene;
            this.NazivPredmeta = _nazivPredmeta;
            this.DatumUpisaOcene = _datumUpisaOcene;
            
        }

    }
    public class DobijaOcenuDemo
    {
        public int? ID { get; set; }
        public string DatumUpisaOcene { get; set; }
     


        public DobijaOcenuDemo()
        { }

        public DobijaOcenuDemo(
         int? _ID,
       
         
         string _datumUpisaOcene
        )
        {
            this.ID = _ID;
            this.DatumUpisaOcene = _datumUpisaOcene;

        }

    }
    public class DobijaOcenuAzur
    {
        
        public string DatumUpisaOcene { get; set; }
        


        public DobijaOcenuAzur()
        { }

        public DobijaOcenuAzur(
       
         string _datumUpisaOcene
        )
        {
            this.DatumUpisaOcene = _datumUpisaOcene;

        }

    }



    ////////////////////////
    ///
    public class BrojTelefonaPregled
    {
        public int BrojTelefona { get; set; }
        public RoditeljPregled Roditelj { get; set; }

        // public Skola.Entiteti.Smer NazivSmera;

        public BrojTelefonaPregled()
        {

        }
        public BrojTelefonaPregled(int brojTelefona, RoditeljPregled roditelj)
        {
            this.BrojTelefona = brojTelefona;
            this.Roditelj = roditelj;
            
        }

    }
    public class BrojTelefonaDemo
    {
        public int BrojTelefona { get; set; }

        // public Skola.Entiteti.Smer NazivSmera;

        public BrojTelefonaDemo()
        {

        }
        public BrojTelefonaDemo(int brojTelefona)
        {
            this.BrojTelefona = brojTelefona;
            

        }

    }
    public class BrojTelefonaAzur
    {

        // public Skola.Entiteti.Smer NazivSmera;

        public BrojTelefonaAzur()
        {

        }
        
    }













}
