namespace TelegramBotForMiners
{
    public class Rootobject
    {
        public int _24hnumreward { get; set; }
        public long _24hreward { get; set; }
        public int apiVersion { get; set; }
        public Config config { get; set; }
        public float currentHashrate { get; set; }
        public string currentLuck { get; set; }
        public float hashrate { get; set; }
        public int pageSize { get; set; }
        public Payment[] payments { get; set; }
        public int paymentsTotal { get; set; }
        public Reward[] rewards { get; set; }
        public int roundShares { get; set; }
        public int sharesInvalid { get; set; }
        public int sharesStale { get; set; }
        public int sharesValid { get; set; }
        public Stats stats { get; set; }
        public Sumreward[] sumrewards { get; set; }
        public long updatedAt { get; set; }
        public Workers workers { get; set; }
        public int workersOffline { get; set; }
        public int workersOnline { get; set; }
        public int workersTotal { get; set; }
    }

    public class Config
    {
        public long allowedMaxPayout { get; set; }
        public int allowedMinPayout { get; set; }
        public int defaultMinPayout { get; set; }
        public string ipHint { get; set; }
        public string ipWorkerName { get; set; }
        public int minPayout { get; set; }
    }

    public class Stats
    {
        public int balance { get; set; }
        public int blocksFound { get; set; }
        public int immature { get; set; }
        public int lastShare { get; set; }
        public long paid { get; set; }
        public int pending { get; set; }
    }

    public class Workers
    {
        public Rig1 rig1 { get; set; }
    }

    public class Rig1
    {
        public int lastBeat { get; set; }
        public float hr { get; set; }
        public bool offline { get; set; }
        public float hr2 { get; set; }
        public int rhr { get; set; }
        public int sharesValid { get; set; }
        public int sharesInvalid { get; set; }
        public int sharesStale { get; set; }
    }

    public class Payment
    {
        public int amount { get; set; }
        public int timestamp { get; set; }
        public string tx { get; set; }
    }

    public class Reward
    {
        public int blockheight { get; set; }
        public int timestamp { get; set; }
        public int reward { get; set; }
        public float percent { get; set; }
        public bool immature { get; set; }
        public bool orphan { get; set; }
    }

    public class Sumreward
    {
        public int inverval { get; set; }
        public long reward { get; set; }
        public int numreward { get; set; }
        public string name { get; set; }
        public int offset { get; set; }
    }
}