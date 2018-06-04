using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace MeteoMobile.Behaviors
{
    public class CompareValidationBehavior:Behavior<Entry>
    {
        public static BindableProperty TextProperty = BindableProperty.Create<CompareValidationBehavior, string>(tc => tc.Text, string.Empty, BindingMode.TwoWay);
        
        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }


        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
            base.OnAttachedTo(bindable);
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            var IsValid = false;
            IsValid = e.NewTextValue == Text || e.OldTextValue == Text;

            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(bindable);
        }
    }
}
