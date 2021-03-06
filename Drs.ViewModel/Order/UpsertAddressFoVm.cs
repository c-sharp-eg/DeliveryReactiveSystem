﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using Drs.Infrastructure.Extensions.Enumerables;
using Drs.Model.Address;
using Drs.Model.Constants;
using Drs.Model.Order;
using Drs.Model.Settings;
using Drs.Model.Shared;
using Drs.Service.ReactiveDelivery;
using Drs.Service.TransferDto;
using Drs.ViewModel.Shared;
using ReactiveUI;

namespace Drs.ViewModel.Order
{
    public class UpsertAddressFoVm : FlyoutBaseVm, IUpsertAddressFoVm, IDataErrorInfo
    {
        private readonly IReactiveDeliveryClient _client;
        private string _zipCode;
        private int? _zipCodeId;
        private IAutoCompleteTextVm _zipCodeSearchVm;
        private AddressControlSetting _controls;
        private bool _isSearchByCode;
        private bool _isSearchByMap;
        private bool _isSearchByWaterfall;
        private bool _isZipCodeSearchEnabled;
        private string _errorSearch;
        private Visibility _errorSearchVisibility;
        private string _errorUpsert;
        private Visibility _errorUpsertVisibility;
        private IReactiveList<ListItemModel> _countries;
        private IReactiveList<ListItemModel> _regionsA;
        private IReactiveList<ListItemModel> _regionsB;
        private IReactiveList<ListItemModel> _regionsC;
        private IReactiveList<ListItemModel> _regionsD;
        private ListItemModel _countrySel;
        private ListItemModel _regionArSel;
        private ListItemModel _regionBrSel;
        private ListItemModel _regionCrSel;
        private ListItemModel _regionDrSel;
        private string _mainAddress;
        private string _reference;
        private string _numExt;
        private List<IReactiveList<ListItemModel>> _lstRegions;
        private Visibility _visibilityMap;
        private Visibility _visibilityManual;
        public string MapRegionCodeSearch { get; private set; }

        public UpsertAddressFoVm(IAutoCompleteTextVm zipCodeSearchVm, IReactiveDeliveryClient client)
        {
            _client = client;
            ZipCodeSearchVm = zipCodeSearchVm;

            ZipCodeSearchVm.Watermark = SettingsData.Constants.AddressUpsertSetting.ZipCode.Title;
            ZipCodeSearchVm.IsFocused = true;

            ZipCodeSearchVm.ExecuteSearch += ExecuteSearchZipCode;
            ZipCodeSearchVm.DoExecuteEvent += OnSelectZipCode;
            Controls = SettingsData.Constants.AddressUpsertSetting;
            IsSearchByCode = false;
            IsSearchByWaterfall = false;
            IsSearchByMap = true;
            ErrorSearch = String.Empty;

            ErrorUpsertVisibility = Visibility.Hidden;

            Countries = new ReactiveList<ListItemModel>();
            RegionsA = new ReactiveList<ListItemModel>();
            RegionsB = new ReactiveList<ListItemModel>();
            RegionsC = new ReactiveList<ListItemModel>();
            RegionsD = new ReactiveList<ListItemModel>();

            _lstRegions = new List<IReactiveList<ListItemModel>> { Countries, RegionsA, RegionsB, RegionsC, RegionsD };

            MapRegionCodeSearch = SettingsData.Address.MapRegionCodeSearch;

            UpsertCommand = ReactiveCommand.CreateAsyncTask(Observable.Return(true), _ => Save());

        }


        public void Save(AddressMapInfoModel model)
        {
            var addressInfo = new AddressInfoModel
            {
                AddressId = Id,
                Country = new ListItemModel { Value = model.Address.Country },
                ExtIntNumber = model.Address.NumExt,
                MainAddress = model.Address.MainAddress,
                Reference = model.Address.Reference,
                RegionA = new ListItemModel { Value = model.Address.RegionA },
                RegionB = new ListItemModel { Value = model.Address.RegionB },
                RegionC = new ListItemModel { Value = model.Address.RegionC },
                RegionD = new ListItemModel { Value = model.Address.RegionD },
                ZipCode = new ListItemModel { Value = model.Address.ZipCode },
                PlaceId = model.PlaceId,
                StoreCoverageIds = model.CoverageStoreIds,
                IsMap = true
            };

            if (model.Position != null)
            {
                addressInfo.Lat = model.Position.Lat;
                addressInfo.Lng = model.Position.Lng;
            }

            MessageBus.Current.SendMessage(new AddressInfoGrid
                {
                    PreId = PreId,
                    AddressInfo = addressInfo
                }, SharedMessageConstants.ORDER_ADDRESSINFO);
        }

        private async Task<Unit> Save()
        {
            await Task.Run(() =>
            {
                if (ValidateFields() == false)
                    return;

                if (!IsOpen)
                    return;

                IsOpen = false;

                MessageBus.Current.SendMessage(
                    new AddressInfoGrid
                    {
                        PreId = PreId,
                        AddressInfo =
                        {
                            AddressId = Id,
                            Country = CountrySel,
                            ExtIntNumber = NumExt,
                            MainAddress = MainAddress,
                            Reference = Reference,
                            RegionA = RegionArSel,
                            RegionB = RegionBrSel,
                            RegionC = RegionCrSel,
                            RegionD = RegionDrSel,
                            ZipCode = new ListItemModel { IdKey = ZipCodeId, Value = ZipCode }
                        }
                    }, SharedMessageConstants.ORDER_ADDRESSINFO);
            });
            return new Unit();
        }

        private bool ValidateFields()
        {
            ErrorUpsert = String.Empty;

            foreach (var control in SettingsData.Constants.AddressUpsertSetting.LstAddressControls)
            {
                if (control.IsEnabled == false)
                    continue;

                switch (control.Name)
                {
                    case SettingsData.Constants.Control.CONTROL_COUNTRY:
                        {
                            if (CountrySel != null && CountrySel.IdKey.HasValue)
                                continue;
                            ErrorUpsert = "Debe seleccionar un(a) " + SettingsData.Constants.AddressUpsertSetting.Country.Title;
                            return false;
                        }
                    case SettingsData.Constants.Control.CONTROL_REGION_A:
                        {
                            if (RegionArSel != null && RegionArSel.IdKey.HasValue)
                                continue;
                            ErrorUpsert = "Debe seleccionar un(a) " + SettingsData.Constants.AddressUpsertSetting.RegionA.Title;
                            return false;
                        }
                    case SettingsData.Constants.Control.CONTROL_REGION_B:
                        {
                            if (RegionBrSel != null && RegionBrSel.IdKey.HasValue)
                                continue;
                            ErrorUpsert = "Debe seleccionar un(a) " + SettingsData.Constants.AddressUpsertSetting.RegionB.Title;
                            return false;
                        }
                    case SettingsData.Constants.Control.CONTROL_REGION_C:
                        {
                            if (RegionCrSel != null && RegionCrSel.IdKey.HasValue)
                                continue;
                            ErrorUpsert = "Debe seleccionar un(a) " + SettingsData.Constants.AddressUpsertSetting.RegionC.Title;
                            return false;
                        }
                    case SettingsData.Constants.Control.CONTROL_REGION_D:
                        {
                            if (RegionDrSel != null && RegionDrSel.IdKey.HasValue)
                                continue;
                            ErrorUpsert = "Debe seleccionar un(a) " + SettingsData.Constants.AddressUpsertSetting.RegionD.Title;
                            return false;
                        }
                    case SettingsData.Constants.Control.CONTROL_MAIN_ADDRESS:
                        {
                            if (String.IsNullOrWhiteSpace(MainAddress) == false)
                                continue;
                            ErrorUpsert = "Debe ingresar un(a) " + SettingsData.Constants.AddressUpsertSetting.MainAddress.Title;
                            return false;
                        }
                    case SettingsData.Constants.Control.CONTROL_NUM_EXT:
                        {

                            if (String.IsNullOrWhiteSpace(NumExt) == false)
                                continue;
                            ErrorUpsert = "Debe ingresar un(a) " + SettingsData.Constants.AddressUpsertSetting.NumExt.Title;
                            return false;
                        }
                }
            }

            return true;
        }


        private void OnSelectZipCode(ListItemModel model)
        {
            if (model == null || String.IsNullOrWhiteSpace(model.Value))
            {
                ErrorSearch = "Debe ingresar un código válido";
                return;
            }

            int iValue;
            int.TryParse(model.Key, out iValue);
            ZipCode = model.Value;
            ZipCodeId = iValue;

            _client.ExecutionProxy.ExecuteRequest<String, String, ResponseMessageData<AddressResponseSearch>, ResponseMessageData<AddressResponseSearch>>
                (model.Value, TransferDto.SameType, SharedConstants.Server.ADDRESS_HUB, SharedConstants.Server.SEARCH_HIERARCHY_BY_ZIPCODE_ADDRESS_HUB_METHOD, TransferDto.SameType)
                .Subscribe(OnResultOkZipCodeHierarchy, OnResultErrorZipCodeHierarchyReady);

        }

        private void OnResultErrorZipCodeHierarchyReady(Exception ex)
        {
            OnResultErrorZipCodeHierarchyReady(ex.Message);
        }

        private void OnResultErrorZipCodeHierarchyReady(String msg)
        {
            ErrorSearch = msg;
        }

        private void OnResultOkZipCodeHierarchy(IStale<ResponseMessageData<AddressResponseSearch>> response)
        {
            if (response.IsStale || response.Data == null)
            {
                OnResultErrorZipCodeHierarchyReady(Resources.Network.ResNetwork.ERROR_NETWORK_DOWN);
                return;
            }

            if (response.Data.IsSuccess == false)
            {
                OnResultErrorZipCodeHierarchyReady(response.Data.Message);
                return;
            }

            RxApp.MainThreadScheduler.Schedule(_ => ProcessZipCodeSearchResponse(response.Data.LstData));

        }

        private void ProcessZipCodeSearchResponse(IEnumerable<AddressResponseSearch> lstData)
        {
            CleanLists();

            foreach (var ahi in lstData)
            {
                AddIfNotExists(ahi.Country, Countries);
                AddIfNotExists(ahi.RegionA, RegionsA);
                AddIfNotExists(ahi.RegionB, RegionsB);
                AddIfNotExists(ahi.RegionC, RegionsC);
                AddIfNotExists(ahi.RegionD, RegionsD);
            }

            if (Countries.Count > 0)
                CountrySel = Countries[0];
            if (RegionsA.Count > 0)
                RegionArSel = RegionsA[0];
            if (RegionsB.Count > 0)
                RegionBrSel = RegionsB[0];
            if (RegionsC.Count > 0)
                RegionCrSel = RegionsC[0];
            if (RegionsD.Count > 0)
                RegionDrSel = RegionsD[0];

        }

        private void CleanLists()
        {
            Countries.Clear();
            RegionsA.Clear();
            RegionsB.Clear();
            RegionsC.Clear();
            RegionsD.Clear();
        }

        private void AddIfNotExists(ListItemModel item, IReactiveList<ListItemModel> lstItems)
        {
            if (item == null)
                return;

            if (lstItems.Any(e => e.IdKey == item.IdKey))
                return;

            lstItems.Add(item);

        }

        private void ExecuteSearchZipCode(string fieldSearch)
        {
            ErrorSearch = String.Empty;
            _client.ExecutionProxy.ExecuteRequest
                <String, String, ResponseMessageData<ListItemModel>, ResponseMessageData<ListItemModel>>
                (fieldSearch, TransferDto.SameType, SharedConstants.Server.ADDRESS_HUB,
                    SharedConstants.Server.SEARCH_BY_ZIPCODE_ADDRESS_HUB_METHOD, TransferDto.SameType)
                .Subscribe(ZipCodeSearchVm.OnResultReady, ZipCodeSearchVm.OnResultError);
        }

        public long PreId { get; set; }

        public long? Id { get; set; }

        public string ZipCode
        {
            get { return _zipCode; }
            set { this.RaiseAndSetIfChanged(ref _zipCode, value); }
        }

        public int? ZipCodeId
        {
            get { return _zipCodeId; }
            set { this.RaiseAndSetIfChanged(ref _zipCodeId, value); }
        }

        public AddressControlSetting Controls
        {
            get { return _controls; }
            set { this.RaiseAndSetIfChanged(ref _controls, value); }
        }

        public IReactiveCommand<Unit> UpsertCommand { get; private set; }

        public string this[string columnName]
        {
            get
            {
                return null;
            }
        }

        public IAutoCompleteTextVm ZipCodeSearchVm
        {
            get { return _zipCodeSearchVm; }
            set { this.RaiseAndSetIfChanged(ref _zipCodeSearchVm, value); }
        }

        public bool IsSearchByCode
        {
            get { return _isSearchByCode; }
            set
            {
                this.RaiseAndSetIfChanged(ref _isSearchByCode, value);
                IsZipCodeSearchEnabled = _isSearchByCode;
            }
        }

        private void ChangeVisibilityMap(bool bEnable)
        {
            RxApp.MainThreadScheduler.Schedule(_ =>
            {
                if (bEnable)
                {
                    VisibilityManual = Visibility.Collapsed;
                    VisibilityMap = Visibility.Visible;
                }
                else
                {
                    VisibilityMap = Visibility.Collapsed;
                    VisibilityManual = Visibility.Visible;
                }
            });
        }

        public bool IsSearchByMap
        {
            get { return _isSearchByMap; }
            set
            {
                this.RaiseAndSetIfChanged(ref _isSearchByMap, value);
                ChangeVisibilityMap(value);
            }
        }

        public bool IsSearchByWaterfall
        {
            get { return _isSearchByWaterfall; }
            set
            {
                this.RaiseAndSetIfChanged(ref _isSearchByWaterfall, value);

                if (_isSearchByWaterfall)
                {
                    RxApp.MainThreadScheduler.Schedule(_ => CleanLists());
                    FillNextList();
                }
            }
        }

        public bool IsZipCodeSearchEnabled
        {
            get { return _isZipCodeSearchEnabled; }
            set
            {
                this.RaiseAndSetIfChanged(ref _isZipCodeSearchEnabled, value);
                if (_isZipCodeSearchEnabled)
                    RxApp.MainThreadScheduler.Schedule(_ => CleanLists());
            }
        }

        public Visibility VisibilityMap
        {
            get { return _visibilityMap; }
            set
            {
                this.RaiseAndSetIfChanged(ref _visibilityMap, value);
            }
        }

        public Visibility VisibilityManual
        {
            get { return _visibilityManual; }
            set
            {
                this.RaiseAndSetIfChanged(ref _visibilityManual, value);
            }
        }

        public string ErrorSearch
        {
            get { return _errorSearch; }
            set
            {
                this.RaiseAndSetIfChanged(ref _errorSearch, value);
                ErrorSearchVisibility = String.IsNullOrWhiteSpace(_errorSearch) ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility ErrorSearchVisibility
        {
            get { return _errorSearchVisibility; }
            set
            {
                this.RaiseAndSetIfChanged(ref _errorSearchVisibility, value);
            }
        }

        public string ErrorUpsert
        {
            get { return _errorUpsert; }
            set
            {
                this.RaiseAndSetIfChanged(ref _errorUpsert, value);
                ErrorUpsertVisibility = String.IsNullOrWhiteSpace(_errorUpsert) ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility ErrorUpsertVisibility
        {
            get { return _errorUpsertVisibility; }
            set
            {
                this.RaiseAndSetIfChanged(ref _errorUpsertVisibility, value);
            }
        }

        public IReactiveList<ListItemModel> Countries
        {
            get { return _countries; }
            set
            {
                this.RaiseAndSetIfChanged(ref _countries, value);

            }
        }

        public IReactiveList<ListItemModel> RegionsA
        {
            get { return _regionsA; }
            set
            {
                this.RaiseAndSetIfChanged(ref _regionsA, value);

            }
        }

        public IReactiveList<ListItemModel> RegionsB
        {
            get { return _regionsB; }
            set
            {
                this.RaiseAndSetIfChanged(ref _regionsB, value);

            }
        }

        public IReactiveList<ListItemModel> RegionsC
        {
            get { return _regionsC; }
            set
            {
                this.RaiseAndSetIfChanged(ref _regionsC, value);

            }
        }

        public IReactiveList<ListItemModel> RegionsD
        {
            get { return _regionsD; }
            set
            {
                this.RaiseAndSetIfChanged(ref _regionsD, value);
            }
        }

        public ListItemModel CountrySel
        {
            get { return _countrySel; }
            set
            {
                this.RaiseAndSetIfChanged(ref _countrySel, value);

                if (_countrySel == null || _countrySel.IdKey.HasValue == false || IsSearchByWaterfall == false)
                    return;
                FillNextList(SettingsData.Constants.Control.CONTROL_REGION_A, (int)_countrySel.IdKey.Value);
            }
        }

        public FranchiseInfoModel Franchise { get; set; }

        public ListItemModel RegionArSel
        {
            get { return _regionArSel; }
            set
            {
                this.RaiseAndSetIfChanged(ref _regionArSel, value);
                if (_regionArSel == null || _regionArSel.IdKey.HasValue == false || IsSearchByWaterfall == false)
                    return;
                FillNextList(SettingsData.Constants.Control.CONTROL_REGION_B, (int)_regionArSel.IdKey.Value);
            }
        }

        public ListItemModel RegionBrSel
        {
            get { return _regionBrSel; }
            set
            {
                this.RaiseAndSetIfChanged(ref _regionBrSel, value);
                if (_regionBrSel == null || _regionBrSel.IdKey.HasValue == false || IsSearchByWaterfall == false)
                    return;
                FillNextList(SettingsData.Constants.Control.CONTROL_REGION_C, (int)_regionBrSel.IdKey.Value);
            }
        }

        public ListItemModel RegionCrSel
        {
            get { return _regionCrSel; }
            set
            {
                this.RaiseAndSetIfChanged(ref _regionCrSel, value);
                if (_regionCrSel == null || _regionCrSel.IdKey.HasValue == false || IsSearchByWaterfall == false)
                    return;
                FillNextList(SettingsData.Constants.Control.CONTROL_REGION_D, (int)_regionCrSel.IdKey.Value);

            }
        }

        public ListItemModel RegionDrSel
        {
            get { return _regionDrSel; }
            set
            {
                this.RaiseAndSetIfChanged(ref _regionDrSel, value);
            }
        }

        public string MainAddress
        {
            get { return _mainAddress; }
            set
            {
                this.RaiseAndSetIfChanged(ref _mainAddress, value);
            }
        }
        public string Reference
        {
            get { return _reference; }
            set
            {
                this.RaiseAndSetIfChanged(ref _reference, value);
            }
        }

        public string NumExt
        {
            get { return _numExt; }
            set
            {
                this.RaiseAndSetIfChanged(ref _numExt, value);
            }
        }


        public void Clean(FranchiseInfoModel franchise)
        {
            IsSearchByWaterfall = false;
            IsSearchByCode = false;
            IsSearchByMap = true;
            Franchise = franchise;
            AddressMapInfo = null;

            RxApp.MainThreadScheduler.Schedule(_ =>
            {
                ErrorUpsert = String.Empty;
                ZipCode = String.Empty;
                ZipCodeId = null;
                ZipCodeSearchVm.Search = String.Empty;
                ZipCodeSearchVm.IsDone = SharedConstants.Client.IS_TRUE;

                CountrySel = null;
                RegionArSel = null;
                RegionBrSel = null;
                RegionCrSel = null;
                RegionDrSel = null;

                MainAddress = String.Empty;
                Reference = String.Empty;
                NumExt = String.Empty;

                PreId = SharedConstants.NULL_ID_VALUE;
                Id = SharedConstants.NULL_ID_VALUE;

            });
        }

        public void Fill(AddressInfoGrid clInfo, FranchiseInfoModel franchise)
        {
            IsSearchByWaterfall = false;
            IsSearchByCode = !clInfo.AddressInfo.IsMap;
            IsSearchByMap = clInfo.AddressInfo.IsMap;
            Franchise = franchise;
            

            RxApp.MainThreadScheduler.Schedule(_ =>
            {
                ErrorUpsert = String.Empty;
                PreId = clInfo.PreId;
                var addr = clInfo.AddressInfo;
                Id = addr.AddressId;


                if (clInfo.AddressInfo.IsMap)
                {
                    AddressMapInfo = new AddressMapInfoModel
                    {
                        Address = new AddressMapModel
                        {
                            Country = addr.Country.Value,
                            MainAddress = addr.MainAddress,
                            NumExt = addr.ExtIntNumber,
                            Reference = addr.Reference,
                            RegionA = addr.RegionA.Value,
                            RegionB = addr.RegionB.Value,
                            RegionC = addr.RegionC.Value,
                            RegionD = addr.RegionD.Value,
                            ZipCode = addr.ZipCode.Value
                        },
                        PlaceId = addr.PlaceId,
                        Position = new PositionModel
                        {
                            Lat = addr.Lat,
                            Lng = addr.Lng,
                        }
                    };
                }
                else
                {
                    ZipCodeId = (int ?)addr.ZipCode.IdKey;
                    ZipCode = addr.ZipCode.Value;
                    ZipCodeSearchVm.Search = ZipCodeId != null ? ZipCode : String.Empty;
                    ZipCodeSearchVm.IsDone = SharedConstants.Client.IS_TRUE;

                    CountrySel = AddAndSelectToControl(addr.Country, Countries);
                    RegionArSel = AddAndSelectToControl(addr.RegionA, RegionsA);
                    RegionBrSel = AddAndSelectToControl(addr.RegionB, RegionsB);
                    RegionCrSel = AddAndSelectToControl(addr.RegionC, RegionsC);
                    RegionDrSel = AddAndSelectToControl(addr.RegionD, RegionsD);
                }

                MainAddress = addr.MainAddress;
                Reference = addr.Reference;
                NumExt = addr.ExtIntNumber;

            });
        }


        private ListItemModel AddAndSelectToControl(ListItemModel item, IReactiveList<ListItemModel> lstItem)
        {
            if (item == null)
                return null;

            lstItem.Add(item);
            return item;
        }

        private void FillNextList(String sNextCmb = "", int iItemSelId = SettingsData.Constants.Entities.NULL_ID_INT)
        {
            ErrorSearch = String.Empty;
            _client.ExecutionProxy.ExecuteRequest
                <AddressQuery, AddressQuery, ResponseMessageData<ListItemModel>, ResponseMessageData<ListItemModel>>
                (new AddressQuery(sNextCmb, iItemSelId), TransferDto.SameType, SharedConstants.Server.ADDRESS_HUB,
                    SharedConstants.Server.FILL_NEXT_LIST_BYNAME_ADDRESS_HUB_METHOD, TransferDto.SameType)
                .Subscribe(OnResultOkFillNextList, OnResultErrorFillNextList);

        }

        private void OnResultErrorFillNextList(Exception ex)
        {
            OnResultErrorZipCodeHierarchyReady(ex.Message);
        }


        private void OnResultOkFillNextList(IStale<ResponseMessageData<ListItemModel>> response)
        {
            if (response.IsStale || response.Data == null)
            {
                OnResultErrorZipCodeHierarchyReady(Resources.Network.ResNetwork.ERROR_NETWORK_DOWN);
                return;
            }

            if (response.Data.IsSuccess == false)
            {
                OnResultErrorZipCodeHierarchyReady(response.Data.Message);
                return;
            }

            RxApp.MainThreadScheduler.Schedule(_ => ProcessWaterfallResponse(response.Data.Message, response.Data.LstData));
        }

        private void ProcessWaterfallResponse(string sControl, IEnumerable<ListItemModel> lstData)
        {
            var listItemModels = lstData as IList<ListItemModel> ?? lstData.ToList();

            switch (sControl)
            {
                case SettingsData.Constants.Control.CONTROL_COUNTRY:
                    {
                        ClearCascadeAddress(Countries);
                        Countries.ClearAndAddRange(listItemModels);
                        return;
                    }
                case SettingsData.Constants.Control.CONTROL_REGION_A:
                    {
                        ClearCascadeAddress(RegionsA);
                        RegionsA.ClearAndAddRange(listItemModels);
                        return;
                    }
                case SettingsData.Constants.Control.CONTROL_REGION_B:
                    {
                        ClearCascadeAddress(RegionsB);
                        RegionsB.ClearAndAddRange(listItemModels);
                        return;
                    }
                case SettingsData.Constants.Control.CONTROL_REGION_C:
                    {
                        ClearCascadeAddress(RegionsC);
                        RegionsC.ClearAndAddRange(listItemModels);
                        return;
                    }
                case SettingsData.Constants.Control.CONTROL_REGION_D:
                    {
                        ClearCascadeAddress(RegionsD);
                        RegionsD.ClearAndAddRange(listItemModels);
                        return;
                    }
            }
        }

        private void ClearCascadeAddress(IReactiveList<ListItemModel> regionIn)
        {
            var bIsFound = false;

            foreach (var region in _lstRegions)
            {
                if (bIsFound == false)
                {
                    if (region == regionIn)
                        bIsFound = true;

                    continue;
                }
                region.Clear();
            }
        }

        public string Error { get { return string.Empty; } }

        public AddressMapInfoModel AddressMapInfo { get; set; }
    }
}
