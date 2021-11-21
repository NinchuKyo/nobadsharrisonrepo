namespace AOERandomizer.Configuration
{
    /// <summary>
    /// Defines the settings for our randomizer app.
    /// </summary>
    public class AOERandomizerSettings
    {
        public string? PathToLogs { get; set; }
        public string? PathToData { get; set; }
        public bool MusicEnabled { get; set; }
        public bool SfxEnabled { get; set; }
    }
}