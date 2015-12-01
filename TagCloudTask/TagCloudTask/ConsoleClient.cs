namespace TagCloudTask
{
    public class ConsoleClient : IClient
    {
        private readonly ITagCloudBuilder tagCloudBuilder;
        private readonly IBitmapWriter bitmapWriter;

        public ConsoleClient(ITagCloudBuilder tagCloudBuilder, IBitmapWriter bitmapWriter)
        {
            this.tagCloudBuilder = tagCloudBuilder;
            this.bitmapWriter = bitmapWriter;
        }

        public void Run()
        {
            var bitmap = tagCloudBuilder.Build();
            bitmapWriter.WriteBitmap(bitmap);
        }
    }
}
