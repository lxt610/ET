using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [Config]
    public partial class StartSceneConfigCategory : ProtoObject
    {
        public static StartSceneConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, StartSceneConfig> dict = new Dictionary<int, StartSceneConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<StartSceneConfig> list = new List<StartSceneConfig>();
		
        public StartSceneConfigCategory()
        {
            Instance = this;
        }
		
        public override void AfterDeserialization()
        {
            foreach (StartSceneConfig config in list)
            {
                this.dict.Add(config.Id, config);
            }
            list.Clear();
            base.AfterDeserialization();
        }
		
        public StartSceneConfig Get(int id)
        {
            this.dict.TryGetValue(id, out StartSceneConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (StartSceneConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, StartSceneConfig> GetAll()
        {
            return this.dict;
        }

        public StartSceneConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.First();
        }
    }

	public partial class StartSceneConfig: IConfig
	{
		[BsonId]
		public int Id { get; set; }
		public int Process;
		public int Zone;
		public string SceneType;
		public string Name;
		public int OuterPort;
	}
}
