using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
	[Config]
	public partial class StartZoneConfigCategory : ProtoObject
	{
		public static StartZoneConfigCategory Instance;
		
		[ProtoIgnore]
		[BsonIgnore]
		private Dictionary<int, StartZoneConfig> dict = new Dictionary<int, StartZoneConfig>();
		
		[BsonElement]
		[ProtoMember(1)]
		private List<StartZoneConfig> list = new List<StartZoneConfig>();
		
		public StartZoneConfigCategory()
		{
			Instance = this;
		}
		
		public override void AfterDeserialization()
		{
			foreach (StartZoneConfig config in list)
			{
				this.dict.Add(config.Id, config);
			}
			list.Clear();
			base.AfterDeserialization();
		}
		
		public StartZoneConfig Get(int id)
		{
			this.dict.TryGetValue(id, out StartZoneConfig item);

			if (item == null)
			{
				throw new Exception($"配置找不到，配置表名: {nameof (StartZoneConfig)}，配置id: {id}");
			}

			return item;
		}
		
		public bool Contain(int id)
		{
			return this.dict.ContainsKey(id);
		}

		public Dictionary<int, StartZoneConfig> GetAll()
		{
			return this.dict;
		}

		public StartZoneConfig GetOne()
		{
			if (this.dict == null || this.dict.Count <= 0)
			{
				return null;
			}
			return this.dict.Values.First();
		}
	}

	public partial class StartZoneConfig: IConfig
	{
		[ProtoMember(1, IsRequired  = true)]
		public int Id { get; set; }
		[ProtoMember(2, IsRequired  = true)]
		public string DBConnection { get; set; }
		[ProtoMember(3, IsRequired  = true)]
		public string DBName { get; set; }
	}
}
