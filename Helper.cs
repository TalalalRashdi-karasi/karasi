namespace Shubak_Website
{
    public static class Helpers
    {
        public static byte[] ConvertToByteArray(this IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}