using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePlatformer

{
    public class Camera
    {
        private Matrix transform;
        private float Zoom = 1f;
        private float rotation = 0;
        private Rectangle Bounds { get; set; }

        public Matrix Transform
        {
            get { return transform; }
        }
        protected float _zoom; // Camera Zoom
        public Matrix _transform; // Matrix Transform
        public Vector2 _pos; // Camera Position
        protected float _rotation; // Camera Rotation

        //public Camera()
        //{
        //    _zoom = 1.0f;
        //    _rotation = 0.0f;
        //    _pos = Vector2.Zero;
        //}
        public Matrix get_transformation()
        {
            _transform =       // Thanks to o KB o for this solution
              Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0)) *
                                         Matrix.CreateRotationZ(_rotation) *
                                         Matrix.CreateScale(new Vector3(_zoom, _zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(viewport.Width * 0.5f, viewport.Height * 0.5f, 0));
            return _transform;
        }
        private Vector2 centre;
        private Viewport viewport;
        public Camera(Viewport newViewport)
        {
            viewport = newViewport;
            if (viewport.Width > 1700 && viewport.Height > 1000)
            {
                _zoom = 1.3f;
            }
            else
            {
                _zoom = 1.0f;
            }
            _rotation = 0.0f;
            _pos = Vector2.Zero;
        }
        public void Update(Vector2 position, int xOffset, int yOffset)
        {
            centre.X = position.X;
            centre.Y = position.Y;




            //transform = Matrix.CreateTranslation(new Vector3(-centre.X + (viewport.Width / 2*Zoom),
            //    -centre.Y + (viewport.Height / 2*Zoom), 0)) * Matrix.CreateScale(Zoom, Zoom, 1.0f);
        }
    }
}
