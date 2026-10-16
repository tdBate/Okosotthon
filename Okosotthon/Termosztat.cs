using System;
using System.Collections.Generic;
using System.Text;

namespace Okosotthon
{
    public class Termosztat : OkosEszkoz, IIdozitheto, IStatisztika
    {
        private double jelenlegiHomerseklet;
        private double celHomerseklet;
        private int hatralevoPerc;
        private readonly List<double> homersekletAdatok = new List<double>();

        public double JelenlegiHomerseklet
        {
            get => jelenlegiHomerseklet;
            private set => jelenlegiHomerseklet = value;
        }

        public double CelHomerseklet
        {
            get => celHomerseklet;
            private set => celHomerseklet = value;
        }

        public int HatralevoPerc
        {
            get => hatralevoPerc;
            private set => hatralevoPerc = value;
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
                if (parancs.StartsWith("FUTES:"))
                {
                    this.IdozitestBeallit(Convert.ToInt32(parancs.Substring(parancs.IndexOf(":") + 1)));
                    return;
                }

                this.CelHomerseklet = Convert.ToDouble(parancs.Substring(parancs.IndexOf(":") + 1));
                this.UtolsoFrissites = DateTime.Now;
            }
            catch
            {
            }

        }

        public void IdozitestBeallit(int perc)
        {
            if (perc < 0) perc = 0;
            this.HatralevoPerc = perc;
            this.UtolsoFrissites = DateTime.Now;
        }

        public int GetMaradtIdo()
        {
            return this.HatralevoPerc;
        }

        public string StatisztikatKeszit()
        {
            this.homersekletAdatok.Add(this.JelenlegiHomerseklet);
            this.homersekletAdatok.Add(this.CelHomerseklet);

            double min = this.homersekletAdatok[0];
            double max = this.homersekletAdatok[0];
            double osszeg = 0;

            foreach (double adat in this.homersekletAdatok)
            {
                if (adat < min) min = adat;
                if (adat > max) max = adat;
                osszeg += adat;
            }

            double atlag = osszeg / this.homersekletAdatok.Count;

            return $"Termosztát statisztika - Mért adatok: {this.homersekletAdatok.Count}, " +
                   $"Átlag: {atlag:F1} °C, Minimum: {min:F1} °C, Maximum: {max:F1} °C, " +
                   $"Hátralévő fűtés: {this.HatralevoPerc} perc";
        }

        public override string AllapotJelentes()
        {
            return $"Jelenlegi hőmérséklet: {this.jelenlegiHomerseklet} ,Célhőmérséklet: {this.celHomerseklet}, Hátralévő idő: {this.hatralevoPerc} perc";
        }

        protected override bool OnTesztFuttatasa()
        {
            return (this.CelHomerseklet > 5 && this.celHomerseklet < 35);
        }

        public override void GyariBeallitasokVisszaallitasa()
        {
            base.GyariBeallitasokVisszaallitasa();
            this.CelHomerseklet = 22.0;
            this.HatralevoPerc = 0;
            this.homersekletAdatok.Clear();
        }

    }
}
