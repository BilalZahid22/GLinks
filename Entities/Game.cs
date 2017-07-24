using System;

namespace Entities
{
    public class Game
    {
        String G_name, Description, SysReq, link, Image, gamply, instlV;
        

        public string DescriptionProp
        {
            get
            {
                return Description;
            }

            set
            {
                Description = value;
            }
        }

        public string GamplyProp
        {
            get
            {
                return gamply;
            }

            set
            {
                gamply = value;
            }
        }

        public string G_nameProp
        {
            get
            {
                return G_name;
            }

            set
            {
                G_name = value;
            }
        }

        public string ImageProp
        {
            get
            {
                return Image;
            }

            set
            {
                Image = value;
            }
        }

        public string InstlVProp
        {
            get
            {
                return instlV;
            }

            set
            {
                instlV = value;
            }
        }

        public string LinkProp
        {
            get
            {
                return link;
            }

            set
            {
                link = value;
            }
        }

        public string SysReqProp
        {
            get
            {
                return SysReq;
            }

            set
            {
                SysReq = value;
            }
        }
    }
}
