using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using General.Input;

namespace General.CostumGameComponents
{

    interface ICDrawable
    {
        int DrawOrder { get; }
        bool IsVisible { get; }

        void Draw(GameTime gametime, SpriteBatch spritebatch);
    }

    public interface ICUpdatable
    {
        bool IsUpdating { get; }

        void Update(GameTime gametime);
    }

    public interface ICGameComponent
    {

    }

    public class CostumGameComponent : ICUpdatable, ICGameComponent
    {
        public bool IsUpdating { get; }

        public CostumGameComponent()
        {
            IsUpdating = true;
            ComponentManegment.Instance.AddComponent(this);
        }

        public virtual void Update(GameTime gametime)
        {

        }

        public virtual void LateUpdate(GameTime gameTime)
        {

        }
    }

    public class DrawableCostumGameComponent : CostumGameComponent, ICDrawable
    {
        public int DrawOrder { get; private set; }
        public bool IsVisible { get; set; }

        public DrawableCostumGameComponent() : base()
        {
            ComponentManegment.Instance.AddDrawComponent(this);
            IsVisible = true;
            SetDrawOrder(0);
        }

        public virtual void Draw(GameTime gametime, SpriteBatch spritebatch)
        {

        }


        public void SetDrawOrder(string layer)
        {
            DrawOrder = ComponentManegment.Instance.SetNewDrawOrder(this, layer);
        }
        public void SetDrawOrder(int order)
        {
            ComponentManegment.Instance.SetNewDrawOrder(this, order);
            DrawOrder = order;
        }

    }

    public class ComponentManegment
    {
        private static ComponentManegment instance;
        List<CostumGameComponent> _updatableComponents = new List<CostumGameComponent>();
        SortedList<int, List<DrawableCostumGameComponent>> _drawableComponenets = new SortedList<int, List<DrawableCostumGameComponent>>();

        private Dictionary<string, int> _layers = new Dictionary<string, int>();

        public static ComponentManegment Instance
        {
            get {
                if (instance == null)
                {
                    instance = new ComponentManegment();
                }
                return instance;
            }
        }

        public void DrawComponents(GameTime gameTime, SpriteBatch spriteBatch)
        {

            for (int i = 0; i < _drawableComponenets.Count; i++)
            {
                var key = _drawableComponenets.Keys[i];
                var item = _drawableComponenets[key];

                for (int j = 0; j < item.Count; j++)
                {
                    if(item[j] != null && item[j].IsVisible)
                        item[j].Draw(gameTime, spriteBatch);
                }
            }
        }
        public void UpdateComponents(GameTime gameTime)
        {
            for (int i = 0; i < _updatableComponents.Count; i++)
            {
                if (_updatableComponents[i] != null)
                {
                    if(_updatableComponents[i].IsUpdating)
                        _updatableComponents[i].Update(gameTime);
                    continue;
                }

                _updatableComponents.RemoveAt(i);
            }
            for (int i = 0; i < _updatableComponents.Count; i++)
            {
                if(_updatableComponents[i] != null)
                    _updatableComponents[i].LateUpdate(gameTime);
            }

        }

        public void AddDrawComponent(DrawableCostumGameComponent component)
        {

            SetNewDrawOrder(component, component.DrawOrder);
            //_updatableComponents.Add(component);
        }
        public void AddComponent(CostumGameComponent component)
        {
            if(!_updatableComponents.Contains(component))
                _updatableComponents.Add(component);
        }

        public int SetNewDrawOrder(DrawableCostumGameComponent component, string layerName)
        {
            var order = 0;
            if (_layers.ContainsKey(layerName))
                order = _layers[layerName];

            SetNewDrawOrder(component, order);
            return order;
        }
        public void SetNewDrawOrder(DrawableCostumGameComponent component, int newDrawOrder)
        {
            if (!_drawableComponenets.ContainsKey(newDrawOrder))
                _drawableComponenets.Add(newDrawOrder, new List<DrawableCostumGameComponent>());

            if (_drawableComponenets.ContainsKey(component.DrawOrder) &&
                _drawableComponenets[component.DrawOrder].Contains(component))
                _drawableComponenets[component.DrawOrder].Remove(component);

            _drawableComponenets[newDrawOrder].Add(component);
        }

        public void Dispose(DrawableCostumGameComponent component)
        {
            _drawableComponenets[component.DrawOrder].Remove(component);
            _updatableComponents.Remove(component);
        }
        public void Dispose(CostumGameComponent component)
        {
            _updatableComponents.Remove(component);
        }

        public void AddDrawLayer(string layerName, int layer)
        {
            if (!_layers.ContainsKey(layerName))
                _layers.Add(layerName, 0);

            _layers[layerName] = layer;
        }
        public int GetDrawLayer(string layerName)
        {
            if (!_layers.ContainsKey(layerName))
                _layers.Add(layerName, 0);

            return _layers[layerName];
        }
    }
}
