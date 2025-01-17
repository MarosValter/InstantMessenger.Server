﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstantMessenger.Client.Base;
using InstantMessenger.Common;
using InstantMessenger.Common.DM;
using InstantMessenger.Common.Flats;
using InstantMessenger.Communication;

namespace InstantMessenger.Client.FindScreen
{
    public class FindModel : ModelBase<IFriendshipsDataManager>
    {
        #region Attributes

        public string Username { get; set; }
        private ObservableCollection<UserFlat> _users;

        public ObservableCollection<UserFlat> Users
        {
            get {return _users;}
            set
            {
                if (_users == value)
                    return;
                _users = value;
                OnPropertyChanged();
            }
        }
        public long SelectedUserOid { get; set; }

        public Action<TransportObject> FindUserAction
        {
            get { return FindUser; }
        }

        #endregion

        #region Overrides

        public void FindUser(TransportObject to)
        {
            //to.Type = Protocol.MessageType.IM_Find;
            to.Add("Username", Username);
        }

        protected override void CreateRequest(TransportObject to)
        {
            //to.Type = Protocol.MessageType.IM_Add;
            to.Add("UserOid", SelectedUserOid);
        }

        protected override void ProcessResponse(TransportObject to)
        {
            var users = to.Get<List<UserFlat>>("Users");
            if (users != null)
                Users = new ObservableCollection<UserFlat>(users);
        }

        #endregion
    }
}
