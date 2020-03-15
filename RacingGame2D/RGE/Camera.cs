using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using System.Drawing;


namespace RGEngine
{
    /// <summary>
    /// A Camera is a device through which the player views the world.
    /// </summary>
    public class Camera
    {
        public Vector2 position;

        public double zoom;

        public double rotation;


        public Camera(Vector2 camPosition, double camZoomValue, double camRotationValue)
        {
            this.position = camPosition;
            this.zoom = camZoomValue;
            this.rotation = camRotationValue;
        }
    }
}
