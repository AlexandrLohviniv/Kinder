using Rg.Plugins.Popup.Pages;
using System;
using Plugin.InputKit.Shared.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using KinderMobile.DTOs;

namespace KinderMobile.PopupChoice
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupChoiceView : PopupPage
    {

        int m_result = -1;
        public int Result { get { return m_result; } set { m_result = value; } }

        public EventHandler selected;

        public PopupChoiceView()
        {
            InitializeComponent();
            this.BindingContext = new PopupChoiceViewModel(this, OptionList, InputFields);
        }


        public static PopupChoiceView CreateChoiceView<T>(T m_struct, InputType inputType = InputType.Default)
        {
            PopupChoiceView popupChoiceView = new PopupChoiceView();

            if (m_struct.GetType().IsEnum)
            {
                int selectedChoice = 0;

                var underlyingType = typeof(T).GetEnumUnderlyingType();
                if (underlyingType == typeof(int))
                {
                    dynamic value = m_struct;
                    selectedChoice = (int)value;
                }


                int i = 0;
                foreach (string options in Enum.GetNames(typeof(T)))
                {
                    Plugin.InputKit.Shared.Controls.RadioButton btn = new Plugin.InputKit.Shared.Controls.RadioButton();
                    btn.Text = options;
                    if (i == selectedChoice)
                        btn.IsChecked = true;
                    btn.CircleColor = Color.Green;

                    popupChoiceView.OptionList.Children.Add(btn);
                    i++;
                }


            }
            else if(m_struct.GetType() == typeof(int))
            {
                dynamic value = m_struct;
                int height = (int)value;

                Plugin.InputKit.Shared.Controls.AdvancedEntry entry = new Plugin.InputKit.Shared.Controls.AdvancedEntry();
                entry.IsRequired = true;
                entry.Title = "Enter your height";
                entry.TextColor = Color.Black;
                entry.Text = height.ToString();
                entry.Placeholder = "height...";
                entry.Annotation = Plugin.InputKit.Shared.AnnotationType.DigitsOnly;
                entry.MaxLength = 3;
                entry.MinLength = 2;
                entry.ValidationMessage = "Enter valid height";
                entry.AnnotationColor = Color.Accent;
                entry.Keyboard = Keyboard.Numeric;
                entry.CompletedCommand = (popupChoiceView.BindingContext as PopupChoiceViewModel).ReadValueCommand;
                popupChoiceView.InputFields.Children.Add(entry);
                popupChoiceView.Layout.Children.Add(new Button() 
                {
                    Text = "Ok",
                    Command = (popupChoiceView.BindingContext as PopupChoiceViewModel).ReadValueCommand
                });
            }
            else if (m_struct.GetType() == typeof(DateTime))
            {
                dynamic value = m_struct;

                DateTime zeroTime = new DateTime(1, 1, 1);

                TimeSpan span = (DateTime.Now - (DateTime)value);

                int years = (zeroTime + span).Year - 1;


                Plugin.InputKit.Shared.Controls.AdvancedSlider slider = new Plugin.InputKit.Shared.Controls.AdvancedSlider();
                slider.MaxValue = 100d;
                slider.MinValue = 0d;
                slider.Title = "Input your age";
                slider.ValuePrefix = "Age: ";
                slider.ValueSuffix = " years";
                slider.TextColor = Color.Black;
                slider.DisplayMinMaxValue = true;
                slider.Value = years;
                popupChoiceView.Layout.Children.Add(slider);
                popupChoiceView.Layout.Children.Add(new Button()
                {
                    Text = "Ok"
                });
            }
            else if (m_struct.GetType() == typeof(string))
            {
                dynamic value = m_struct;
                string height = (string)value;

                Plugin.InputKit.Shared.Controls.AdvancedEntry entry = new Plugin.InputKit.Shared.Controls.AdvancedEntry();
                entry.IsRequired = true;
                entry.Title = "Enter value";
                entry.TextColor = Color.Black;
                entry.Text = height.ToString();
                entry.Placeholder = "value...";
                entry.Annotation = Plugin.InputKit.Shared.AnnotationType.LettersOnly;
                entry.MaxLength = 30;
                entry.MinLength = 2;
                entry.ValidationMessage = "Enter valid value";
                entry.AnnotationColor = Color.Accent;
                entry.Keyboard = Keyboard.Text;
                popupChoiceView.InputFields.Children.Add(entry);
                popupChoiceView.Layout.Children.Add(new Button()
                {
                    Text = "Ok"
                });
            }
            return popupChoiceView;

        }
    }
}