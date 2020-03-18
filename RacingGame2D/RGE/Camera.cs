using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
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

        
        public static void SetView(int gameWidth, int gameHeight)
        {
            // may need to add GL.Viewport()
            // Setup the OpenGL workflow to set the view.
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            // Put the view in the center of the window.
            // Left, Rigth, Bottom, Top.
            GL.Ortho(-gameWidth / 2f, gameWidth / 2f, gameHeight / 2f, -gameHeight / 2f, 0f, 1f);
        }
    }
}
