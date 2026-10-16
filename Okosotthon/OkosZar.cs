using System;
using System.Collections.Generic;
using System.Text;

namespace Okosotthon
{
    public class OkosZar : OkosEszkoz, IHangjelzo, IStatisztika
    {
        public bool zartE = true;

        public bool ZartE
        {
            get => zartE;
            private set => zartE = value;
        }

        private string pinKod;

        private int nyitasokSzama;
        private int hibasKiserletekSzama;
        private int csipogasokSzama;
        private bool veszjelzesAktiv;

        public int NyitasokSzama
        {
            get => nyitasokSzama;
            private set => nyitasokSzama = value;
        }

        public int HibasKiserletekSzama
        {
            get => hibasKiserletekSzama;
            private set => hibasKiserletekSzama = value;
        }

        public int CsipogasokSzama
        {
            get => csipogasokSzama;
            private set => csipogasokSzama = value;
        }

        public bool VeszjelzesAktiv
        {
            get => veszjelzesAktiv;
            private set => veszjelzesAktiv = value;
        }

        public OkosZar(string azonosito, string nev, string pinKod)
            : base(azonosito, nev)
        {
            this.pinKod = pinKod;
        }

        public override void ParancsVegrehajtasa(string parancs)
        {
            if (parancs.StartsWith("NYITAS:"))
            {
                string megadottPin = parancs.Substring(7);
                if (megadottPin == this.pinKod)
                {
                    this.zartE = false;
                    this.NyitasokSzama++;
                    this.VeszjelzesAktiv = false;
                }
                else
                {
                    this.HibasKiserletekSzama++;
                    this.Csipog(2);
                    this.VeszjelzesInditasa();
                }
            }
            else if (parancs == "ZARAS")
            {
                this.zartE = true;
            }
            else if (parancs == "VESZJELZES_LEALLITAS")
            {
                this.VeszjelzesAktiv = false;
            }
        }

        public void Csipog(int hosszu)
        {
            this.CsipogasokSzama++;
        }

        public void VeszjelzesInditasa()
        {
            this.VeszjelzesAktiv = true;
        }

        public string StatisztikatKeszit()
        {
            return $"OkosZár statisztika - Sikeres nyitások: {this.NyitasokSzama}, " +
                   $"Hibás PIN kísérletek: {this.HibasKiserletekSzama}, " +
                   $"Csipogások: {this.CsipogasokSzama}, " +
                   $"Vészjelzés: {(this.VeszjelzesAktiv ? "AKTÍV" : "inaktív")}";
        }

        public override string AllapotJelentes()
        {
            return this.zartE ? "ZÁRVA" : "NYITVA";
        }

        protected override bool OnTesztFuttatasa()
        {
            return true;
        }

        public override void GyariBeallitasokVisszaallitasa()
        {
            base.GyariBeallitasokVisszaallitasa();
            this.pinKod = "0000";
            this.zartE = true;
            this.NyitasokSzama = 0;
            this.HibasKiserletekSzama = 0;
            this.CsipogasokSzama = 0;
            this.VeszjelzesAktiv = false;
        }
    }
}