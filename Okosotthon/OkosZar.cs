using System;
using System.Collections.Generic;
using System.Text;

namespace Okosotthon
{
    public class OkosZar : OkosEszkoz
    {
        public bool zartE = true;
        
        public bool ZartE
        {
            get => zartE;
            private set=> zartE = value;
        }
        
        private string pinKod;

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
                }
            }
            else if (parancs == "ZARAS")
            {
                this.zartE = true;
            }
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
        }
    }
}