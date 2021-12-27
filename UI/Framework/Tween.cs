using System;
using System.Collections.Generic;
using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
    public static class Tween
    {
        public static Tween<T> Property<T>(T value, Action<T> setter)
        {
            Tween<T> tween = CreateTweenFor<T>(value);
            tween.OnUpdated += (t) => setter(t.Current);
            return tween;
        }

        private static Tween<T> CreateTweenFor<T>(T initial)
        {
            if (initial is float f) return new FloatTween(f) as Tween<T>;
            else if (initial is int i) return new IntTween(i) as Tween<T>;
            else if (initial is Rectangle r) return new RectangleTween(r) as Tween<T>;
            else if (initial is Color color) return new ColorTween(color) as Tween<T>;
            else throw new Exception("No tween available for type " + typeof(T).Name);
        }
    }

    public abstract class Tween<T> : IUpdateable
    {
        public event Action<Tween<T>> OnUpdated;

        public float Speed { get; private set; }

        public T Current { get; protected set; }
        public T Initial { get; private set; }
        public T Target { get; private set; }
        public float Progress { get; private set; }
        public float CurrentDuration { get; private set; }

        private readonly List<(T target, float speed, Action action)> tweens = new List<(T target, float speed, Action action)>();

        public Tween(T initial)
        {
            Initial = initial;
            Current = initial;
        }

        public Tween<T> BindUpdated(Action onUpdated)
        {
            OnUpdated += (tween) => onUpdated();
            return this;
        }

        public Tween<T> BindUpdated(Action<Tween<T>> onUpdated)
        {
            OnUpdated += onUpdated;
            return this;
        }

        public Tween<T> StartWith(T with)
        {
            Initial = with;
            Current = with;
            return this;
        }

        public Tween<T> To(T target, float speed)
        {
            tweens.Add((target, speed, null));
            return this;
        }

        public Tween<T> Do(Action action)
        {
            if (tweens.Count > 0)
                tweens.Add((tweens[tweens.Count - 1].target, 0, action));
            else
                tweens.Add((Initial, 0, action));

            return this;
        }

        public Tween<T> Delay(float time)
        {
            if (tweens.Count <= 0) return To(Initial, time);
            else return To(tweens[tweens.Count - 1].target, time);
        }

        public void Start()
        {
            Updateables.RegisterGlobalUpdateable(this);
            MoveToNextTarget();
        }

        public void Update(float deltaTime)
        {
            if (Speed <= 0) // If the speed is 0, move to target instantaneously
            {
                Current = Target;

                if (tweens.Count > 0) // If we have any tweens to do, move to the bottom one and remove it from the list
                {
                    MoveToNextTarget();
                }
                else
                {
                    Updateables.UnregisterGlobalUpdateable(this);
                    Current = Target;
                }
            }
            else
            {
                CurrentDuration += deltaTime;
                Progress = CurrentDuration / Speed;

                if (Progress >= 1) Progress = 1;

                DoLerp();

                if (Progress >= 1) // When true, the tween is done and we move onto the next one
                {
                    if (tweens.Count > 0) // If we have any tweens to do, move to the bottom one and remove it from the list
                    {
                        MoveToNextTarget();
                    }
                    else
                    {
                        Updateables.UnregisterGlobalUpdateable(this);
                        Current = Target;
                    }
                }
            }

            OnUpdated?.Invoke(this);
        }

        private void MoveToNextTarget()
        {
            // Setup new values for lerp
            Initial = Current;
            Target = tweens[0].target;
            Speed = tweens[0].speed;
            Progress = 0;
            CurrentDuration = 0;

            // Execute the action if there is one
            tweens[0].action?.Invoke();

            tweens.RemoveAt(0);
        }

        protected float LerpF(float initialF, float targetF)
        {
            return initialF * (1 - Progress) + targetF * Progress;
        }

        protected abstract void DoLerp();
    }

    public class FloatTween : Tween<float>
    {
        public FloatTween(float start) : base(start)
        {
        }

        protected override void DoLerp()
        {
            Current = LerpF(Initial, Target);
        }
    }

    public class IntTween : Tween<int>
    {
        public IntTween(int start) : base(start)
        {
        }

        protected override void DoLerp()
        {
            Current = (int)LerpF(Initial, Target);
        }
    }

    public class RectangleTween : Tween<Rectangle>
    {
        public RectangleTween(Rectangle start) : base(start)
        {
        }

        protected override void DoLerp()
        {
            Current = new Rectangle(
                LerpF(Initial.x, Target.x),
                LerpF(Initial.y, Target.y),
                LerpF(Initial.width, Target.width),
                LerpF(Initial.height, Target.height)
            );
        }
    }

    public class ColorTween : Tween<Color>
    {
        public ColorTween(Color initial) : base(initial)
        {
        }

        protected override void DoLerp()
        {
            int r = (int)LerpF(Initial.r, Target.r);
            int g = (int)LerpF(Initial.g, Target.g);
            int b = (int)LerpF(Initial.b, Target.b);
            int a = (int)LerpF(Initial.a, Target.a);

            Current = new Color(r, g, b, a);
        }
    }
}