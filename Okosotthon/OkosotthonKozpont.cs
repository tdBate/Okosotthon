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
    }
}
