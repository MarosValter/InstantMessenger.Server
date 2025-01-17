﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using InstantMessenger.Client.Base;
using InstantMessenger.Common;
using InstantMessenger.Common.DM;
using InstantMessenger.Common.Flats;
using InstantMessenger.Communication;

namespace InstantMessenger.Client.RequestScreen
{
    public class RequestModel : ModelBase<IFriendshipsDataManager>
    {
        #region Attributes

        public long SelectedUserOid { get; set; }
        private ObservableCollection<RequestFlat> _requests;

        public ObservableCollection<RequestFlat> Requests
        {
            get {return _requests;}
            set
            {
                if (_requests == value)
                    return;
                _requests = value;
                OnPropertyChanged();
            }
        }

        public Action<TransportObject> DeleteRequestAction { get { return DeleteRequest; } }

        #endregion

        public RequestModel()
        {
            InitMethod = i => i.GetRequests;
            MainMethod = i => i.AcceptRequest;
            CustomMethod = i => i.DeleteRequest;
            GetInitData();
        }

        #region Overrides

        protected override void CreateInitRequest(TransportObject to)
        {
            //to.Type = Protocol.MessageType.IM_GetRequests;
        }
        protected override void CreateRequest(TransportObject to)
        {
            //to.Type = Protocol.MessageType.IM_AcceptRequest;
            to.Add("UserOid", SelectedUserOid);
        }

        public void DeleteRequest(TransportObject to)
        {
            //to.Type = Protocol.MessageType.IM_DeleteRequest;
            to.Add("UserOid", SelectedUserOid);
        }

        protected override void ProcessResponse(TransportObject to)
        {
            Requests = new ObservableCollection<RequestFlat>(to.Get<List<RequestFlat>>("Requests"));
        }

        #endregion
    }
}
