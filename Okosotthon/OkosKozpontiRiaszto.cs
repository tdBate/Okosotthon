using System;
using System.Collections.Generic;
using System.Text;

namespace Okosotthon
{
    public class OkosKozpontiRiaszto : OkosEszkoz, IIdozitheto, IHangjelzo, IStatisztika
    {
        private bool aktivE;
        private int hatralevoPerc;
        private int csipogasokSzama;
        private int riasztasokSzama;
        private readonly List<string> esemenynaplo = new List<string>();

        public bool AktivE
        {
            get => aktivE;
            private set => aktivE = value;
        }

        public int HatralevoPerc
        {
            get => hatralevoPerc;
            private set => hatralevoPerc = value;
        }

        public int CsipogasokSzama
        {
            get => csipogasokSzama;
            private set => csipogasokSzama = value;
        }

        public int RiasztasokSzama
        {
            get => riasztasokSzama;
            private set => riasztasokSzama = value;
        }

        public OkosKozpontiRiaszto(string azonosito, string nev)
            : base(azonosito, nev)
        {
        }

        public override void ParancsVegrehajtasa(string parancs)
        {
            if (parancs.StartsWith("IDOZITES:"))
            {
                string ertek = parancs.Substring(parancs.IndexOf(":") + 1);
                try
                {
                    this.IdozitestBeallit(Convert.ToInt32(ertek));
                }
                catch
                {
                }
            }
            else if (parancs == "RIASZTAS")
            {
                this.VeszjelzesInditasa();
            }
            else if (parancs == "LEALLITAS")
            {
                this.aktivE = false;
            }
            else if (parancs.StartsWith("CSIPOG:"))
            {
                string ertek = parancs.Substring(parancs.IndexOf(":") + 1);
                try
                {
                    this.Csipog(Convert.ToInt32(ertek));
                }
                catch
                {
                }
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

        public void Csipog(int hosszu)
        {
            this.CsipogasokSzama++;
            this.esemenynaplo.Add($"Csipogás: {hosszu}");
            this.UtolsoFrissites = DateTime.Now;
        }

        public void VeszjelzesInditasa()
        {
            this.aktivE = true;
            this.RiasztasokSzama++;
            this.esemenynaplo.Add("Vészjelzés indítva");
            this.UtolsoFrissites = DateTime.Now;
        }

        public string StatisztikatKeszit()
        {
            return $"Központi riasztó statisztika - Riasztások: {this.RiasztasokSzama}, " +
                   $"Csipogások: {this.CsipogasokSzama}, Naplózott események: {this.esemenynaplo.Count}, " +
                   $"Hátralévő idő: {this.HatralevoPerc} perc, " +
                   $"Állapot: {(this.aktivE ? "AKTÍV" : "készenlét")}";
        }

        public override string AllapotJelentes()
        {
            return this.aktivE ? "VÉSZJELZÉS AKTÍV" : "KÉSZENLÉT";
        }

        protected override bool OnTesztFuttatasa()
        {
            return true;
        }

        public override void GyariBeallitasokVisszaallitasa()
        {
            base.GyariBeallitasokVisszaallitasa();
            this.aktivE = false;
            this.HatralevoPerc = 0;
            this.CsipogasokSzama = 0;
            this.RiasztasokSzama = 0;
            this.esemenynaplo.Clear();
        }
    }
}
