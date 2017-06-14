using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace 推箱子
{
    class GameImages
    {
        public static int sideLength = 50;//图片大小设成50*50
        static Image imgPerson, imgWall, imgEmpty, imgTarget, imgBox, imgPersonTarget,imgNone,imgBoxTarget;
        static GameImages()
        {
            //初始化图片
            imgPerson = Image.FromFile("imgPerson.png");
            imgWall = Image.FromFile("imgWall.png");
            imgEmpty = Image.FromFile("imgEmpty.png");
            imgTarget = Image.FromFile("imgTarget.png");
            imgBox = Image.FromFile("imgBox.png");
            imgPersonTarget = Image.FromFile("imgPersonTarget.png");
            imgNone = Image.FromFile("imgNone.png");
            imgBoxTarget = Image.FromFile("imgBoxTarget.png");
        }

        //根据格子状态返回相应的图片
        public static Image getImage(State state)
        {
            if (state == State.Empty) return imgEmpty;
            else if (state == State.Person) return imgPerson;
            else if (state == State.Box) return imgBox;
            else if (state == State.PersonTarget) return imgPersonTarget;
            else if (state == State.Wall) return imgWall;
            else if (state == State.Target) return imgTarget;
            else if (state == State.None) return imgNone;
            else if (state == State.BoxTarget) return imgBoxTarget;
            else return null;
        }
    }
}
