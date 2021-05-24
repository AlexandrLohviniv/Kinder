using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KinderMobile.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessageViewControl : ContentView
    {
        public static readonly BindableProperty SenderTypeProperty;
        public static readonly BindableProperty CardContentProperty;
        public static readonly BindableProperty SenderInfoProperty;

        public string CardContent 
        {
            get 
            {
                return base.GetValue(CardContentProperty)?.ToString();
            }
            set 
            {
                base.SetValue(CardContentProperty, value);
            }
        }

        public string SenderInfo
        {
            get
            {
                return base.GetValue(SenderInfoProperty)?.ToString();
            }
            set
            {
                base.SetValue(SenderInfoProperty, value);
            }
        }


        public MessageSenderType SenderType 
        {
            get 
            {
                return (MessageSenderType)base.GetValue(SenderTypeProperty);
            }
            set 
            {
                base.SetValue(SenderTypeProperty, value);
            } 
        }

        static MessageViewControl()
        {
            SenderTypeProperty = BindableProperty.Create(
                nameof(SenderType),
                typeof(MessageSenderType),
                typeof(MessageViewControl),
                defaultValue: MessageSenderType.None,
                defaultBindingMode: BindingMode.OneWay,
                propertyChanged: SenderTypePropertyChanged);

            CardContentProperty = BindableProperty.Create(
                nameof(CardContent),
                typeof(string),
                typeof(MessageViewControl),
                defaultValue: "",
                defaultBindingMode: BindingMode.OneWay,
                propertyChanged: CardContentPropertyChaged);


            SenderInfoProperty = BindableProperty.Create(
                nameof(SenderInfo),
                typeof(string),
                typeof(MessageViewControl),
                defaultValue: "",
                defaultBindingMode: BindingMode.OneWay,
                propertyChanged: SenderInfoPropertyChaged);


        }

        private static void SenderInfoPropertyChaged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (MessageViewControl)bindable;
            control.SenderContent.Text = (string)newValue;
        }

        private static void CardContentPropertyChaged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (MessageViewControl)bindable;
            control.Content.Text = (string)newValue;
        }

        private static void SenderTypePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (MessageViewControl)bindable;
            control.SenderType = (MessageSenderType)newValue;
            switch (control.SenderType)
            {
                case MessageSenderType.Me:
                    control.Card.ColumnDefinitions = (ColumnDefinitionCollection)new ColumnDefinitionCollectionTypeConverter().ConvertFromInvariantString("*, 3*");
                    Grid.SetColumn(control.CardFrame, 1);
                    Grid.SetColumn(control.SenderContent, 0);
                    control.SenderContent.HorizontalOptions = LayoutOptions.End;
                    break;
                case MessageSenderType.Other:
                    control.Card.ColumnDefinitions = (ColumnDefinitionCollection)new ColumnDefinitionCollectionTypeConverter().ConvertFromInvariantString("3*, *");
                    Grid.SetColumn(control.CardFrame, 0);
                    Grid.SetColumn(control.SenderContent, 1);
                    control.SenderContent.HorizontalOptions = LayoutOptions.Start;
                    break;
                default:
                    control.Card.ColumnDefinitions = (ColumnDefinitionCollection)new ColumnDefinitionCollectionTypeConverter().ConvertFromInvariantString("*, 3*");
                    Grid.SetColumn(control.CardFrame, 1);
                    Grid.SetColumn(control.SenderContent, 0);
                    control.SenderContent.HorizontalOptions = LayoutOptions.End;
                    break;
            }
        }

        public MessageViewControl()
        {
            InitializeComponent();
        }
    }
}