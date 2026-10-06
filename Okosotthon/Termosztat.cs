using System;
using System.Collections.Generic;
using System.Text;

namespace Okosotthon
{
    public class Termosztat : OkosEszkoz
    {
        private double jelenlegiHomerseklet;
        private double celHomerseklet;
        
        public double JelenlegiHomerseklet
        {
            get => jelenlegiHomerseklet;
            private set=> jelenlegiHomerseklet = value;
        }
        
        public double CelHomerseklet
        {
            get => celHomerseklet;
            private set => celHomerseklet = value;
        }
        
        public Termosztat(string azonosito, string nev, double celHomerseklet)
        : base(azonosito, nev)
        {
            this.celHomerseklet = celHomerseklet;
        }

        public override void ParancsVegrehajtasa(string parancs)
        {
            try
            {
                this.CelHomerseklet = Convert.ToDouble(parancs.Substring(parancs.IndexOf("#")).Replace('.',','));
            }
            catch
            {
            }

        }

        public override string AllapotJelentes()
        {
            return $"Jelenlegi hőmérséklet: {this.jelenlegiHomerseklet} ,Célhőmérséklet: {this.celHomerseklet}";
        }

        protected override bool OnTesztFuttatasa()
        {
            return (this.CelHomerseklet > 5 && this.celHomerseklet < 35);
        }

    }
}
