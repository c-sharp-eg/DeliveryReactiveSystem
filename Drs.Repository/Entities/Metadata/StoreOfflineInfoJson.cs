﻿using System;
using Drs.Infrastructure.Extensions;
using Drs.Model.Annotations;

namespace Drs.Repository.Entities.Metadata
{
    public static class StoreOfflineInfoJson
    {
        [UsedImplicitly]
        private static FranchiseStoreOffLine _modelEnt = new FranchiseStoreOffLine();
        public static readonly string Key = _modelEnt.PropertyName(e => e.FranchiseStoreOffLineId);

        public static readonly string Columns = String.Join(",", new[]
        {
            _modelEnt.PropertyName(e => e.FranchiseStoreOffLineId),
            _modelEnt.PropertyName(e => e.DateTimeStart),
            _modelEnt.PropertyName(e => e.Duration),
            _modelEnt.PropertyName(e => e.DateTimeEnd),
        });

        public static StoreOfflineInfoDto DynamicToDto(dynamic data)
        {
            var model = new StoreOfflineInfoDto
            {
                FranchiseStoreOffLineId = data.FranchiseStoreOffLineId,
                DateTimeStartInfo = data.DateTimeStart,
                Duration = data.Duration,
                DateTimeEndInfo = data.DateTimeEnd,
            };

            return model;
        }
    }
}