using KinderMobile.DTOs;
using PanCardView.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KinderMobile.NavMenu.MatchPage
{
	public class MatchPageViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private ObservableCollection<UserDto> m_items;
		public ObservableCollection<UserDto> Items 
		{
			get 
			{
				if (m_items == null)
					m_items = new ObservableCollection<UserDto>();
				return m_items;
			}
			set 
			{
				m_items = value;
				OnPropertyChanged("Items");
			}
		}
		
		
		private int _currentIndex;
		private int _imageCount = 1078;

		private bool m_isRightToLeft = true;
		public bool IsRightToLeft 
		{
			get 
			{
				return m_isRightToLeft; 
			}
			set 
			{
				m_isRightToLeft = value;
				OnPropertyChanged("IsRightToLeft");

			} 
		}

		public MatchPageViewModel()
		{
			Task.Run(async ()=> 
			{
				Items = await HttpClientImpl.Instance.GetUserByPreference(HttpClientImpl.Instance.UserId, CurrentUser.Instance.UserPreferenceDto);
			}).Wait();

			RemoveCurrentItemByLikeCommand = new Command<int>(async (param) =>
			{
				if (!Items.Any())
				{
					return;
				}
				await HttpClientImpl.Instance.SendLike(HttpClientImpl.Instance.UserId, param);

				IsRightToLeft = false;
				Items.RemoveAt(CurrentIndex.ToCyclicalIndex(Items.Count));
			});

			RemoveCurrentItemByDislikeCommand = new Command(() =>
			{
				if (!Items.Any())
				{
					return;
				}
				IsRightToLeft = true;
				Items.RemoveAt(CurrentIndex.ToCyclicalIndex(Items.Count));
			});

		}

		public ICommand PanPositionChangedCommand { get; }

		public ICommand RemoveCurrentItemByLikeCommand { get; }
		
		public ICommand RemoveCurrentItemByDislikeCommand { get; }

		public ICommand GoToLastCommand { get; }

		public int CurrentIndex
		{
			get => _currentIndex;
			set
			{
				_currentIndex = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentIndex)));
			}
		}

		public bool IsAutoAnimationRunning { get; set; }

		public bool IsUserInteractionRunning { get; set; }

		

		private string CreateSource()
		{
			var source = $"https://picsum.photos/500/500?image={_imageCount}";
			return source;
		}

		protected void OnPropertyChanged(string propName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propName));
		}
	}
}
