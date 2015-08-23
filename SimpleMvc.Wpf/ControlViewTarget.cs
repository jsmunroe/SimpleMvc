using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SimpleMvc.Wpf
{
    public abstract class ControlViewTarget : ViewTarget<FrameworkElement>
    {
        public static readonly DependencyProperty RegisterAsProperty = DependencyProperty.RegisterAttached("RegisterAs", typeof(string), typeof(ControlViewTarget), new FrameworkPropertyMetadata(null, OnRegisterAsPropertyChanged));

        private static readonly Dictionary<string, ControlViewTarget> _targetsByName = new Dictionary<string, ControlViewTarget>();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="a_targetName">Target name.</param>
        protected ControlViewTarget(string a_targetName)
        {
            _targetsByName[a_targetName] = this;
        }

        /// <summary>
        /// Register the given content control (<paramref name="a_control"/>) in this target.
        /// </summary>
        /// <param name="a_control">Content control.</param>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_control"/>" is null.</exception>
        public abstract void RegisterControl(ContentControl a_control);

        /// <summary>
        /// Get the value of the RegisterAs attached dependency property.
        /// </summary>
        /// <param name="ob">Dependency object containing the value.</param>
        /// <returns>Attached dependency property value.</returns>
        public static string GetRegisterAs(DependencyObject ob)
        {
            if (ob == null)
                return default(string);

            var value = ob.GetValue(RegisterAsProperty);

            if (value is string == false)
                return default(string);

            return (string)ob.GetValue(RegisterAsProperty);
        }

        /// <summary>
        /// Set the value of the RegisterAs attached dependency property.
        /// </summary>
        /// <param name="ob">Dependency object containing the value.</param>
        /// <param name="value">Attached dependency property value.</param>
        public static void SetRegisterAs(DependencyObject ob, string value)
        {
            ob.SetValue(RegisterAsProperty, value);
        }

        /// <summary>
        /// When the RegisterAsProperty dependency property is changed.
        /// </summary>
        private static void OnRegisterAsPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var control = obj as ContentControl;

            if (control == null)
                return;

            var name = e.NewValue as string;

            if (name == null || !_targetsByName.ContainsKey(name))
                return;

            var target = _targetsByName[name];
            target.RegisterControl(control);
        }
    }

    public class ControlViewTarget<TControl> : ControlViewTarget
        where TControl : ContentControl
    {
        protected TControl _control = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="a_targetName">Target name.</param>
        public ControlViewTarget(string a_targetName) 
            : base(a_targetName)
        {

        }

        /// <summary>
        /// Set this target to the given view (<paramref name="a_view"/>).
        /// </summary>
        /// <param name="a_view">View object.</param>
        public override void SetView(FrameworkElement a_view)
        {
            _control?.Dispatcher.Invoke(() =>
            {
                _control.Content = a_view;
            });
        }

        /// <summary>
        /// Register the given control (<paramref name="a_control"/>) in this target.
        /// </summary>
        /// <param name="a_control">Control.</param>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_control"/>" is null.</exception>
        public override void RegisterControl(ContentControl a_control)
        {
            #region Argument Validation

            if (a_control == null)
                throw new ArgumentNullException(nameof(a_control));

            #endregion

            if (a_control is TControl == false)
                throw new InvalidOperationException($"This target does not support a control of type '{a_control.GetType().FullName}'");

            _control = (TControl) a_control;
        }
    }
}
