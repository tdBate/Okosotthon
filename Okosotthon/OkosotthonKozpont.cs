using System;
using System.Collections.Generic;
using System.Text;

namespace Okosotthon
{
    public class OkosotthonKozpont
    {
        private readonly List<OkosEszkoz> eszkozok = new List<OkosEszkoz>();

        public void EszkozHozzaadasa(OkosEszkoz eszkoz)
        {
            this.eszkozok.Add(eszkoz);
        }

        public void OsszesCsatlakoztatasa()
        {
            foreach (OkosEszkoz eszkoz in this.eszkozok)
            {
                eszkoz.Csatlakozas();
            }
        }

        public int RendszerDiagnosztikaFuttatasa()
        {
            int sikeres = 0;
            foreach (OkosEszkoz eszkoz in this.eszkozok)
            {
                if (eszkoz.DiagnosztikaFuttatasa())
                {
                    sikeres++;
                }
            }
            return sikeres;
        }

        public int OsszesVeszjelzesInditasa()
        {
            int megszolitott = 0;
            foreach (OkosEszkoz eszkoz in this.eszkozok)
            {
                if (eszkoz is IHangjelzo hangjelzo)
                {
                    hangjelzo.VeszjelzesInditasa();
                    megszolitott++;
                }
            }
            return megszolitott;
        }

        public int OsszesCsipoges(int hosszu)
        {
            int megszolitott = 0;
            foreach (OkosEszkoz eszkoz in this.eszkozok)
            {
                IHangjelzo hangjelzo = eszkoz as IHangjelzo;
                if (hangjelzo != null)
                {
                    hangjelzo.Csipog(hosszu);
                    megszolitott++;
                }
            }
            return megszolitott;
        }

        public List<string> OsszesStatisztikaLekerdezese()
        {
            List<string> statisztikak = new List<string>();
            foreach (OkosEszkoz eszkoz in this.eszkozok)
            {
                if (eszkoz is IStatisztika statisztika)
                {
                    statisztikak.Add($"{eszkoz.Nev}: {statisztika.StatisztikatKeszit()}");
                }
            }
            return statisztikak;
        }

        public int OsszesIdozitesBeallitasa(int perc)
        {
            int megszolitott = 0;
            foreach (OkosEszkoz eszkoz in this.eszkozok)
            {
                IIdozitheto idozitheto = eszkoz as IIdozitheto;
                if (idozitheto != null)
                {
                    idozitheto.IdozitestBeallit(perc);
                    megszolitott++;
                }
            }
            return megszolitott;
        }
    }
}
