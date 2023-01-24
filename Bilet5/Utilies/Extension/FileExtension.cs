namespace Bilet5.Utilies.Extension
{
    public static class FileExtension
    {
        public static bool CheckTpye(this IFormFile file, string type)
        {
            return file.ContentType.Contains(type);
        }
        public static bool CheckSize(this IFormFile file, int kb)
        {
            return kb * 1024 > file.Length;
        }
        public static string SaveFile(this IFormFile file, string path)
        {
            string fileName = ChangeFileName(file.FileName);
            using (FileStream fs = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                file.CopyTo(fs);
                return fileName;
            }
        }
        static string ChangeFileName(string oldName)
        {
            string extension = oldName.Substring(oldName.LastIndexOf('.'));
            if (oldName.Length<32)
            {
                oldName = oldName.Substring(0, oldName.LastIndexOf('.'));
            }
            else
            {
                oldName = oldName.Substring(0, 31);
            }
            return Guid.NewGuid() + oldName + extension;
        }
        public static string CheckValidate(this IFormFile file, string type, int kb)
        {
            string result = "";
            if (!file.CheckSize(kb))
            {
                result += $"{file.FileName} faylin hecmi {kb} kb-dan artiq olmamalidir";
            }
            if (!file.CheckTpye(type))
            {
                result += $"{file.FileName} faylinin tipi sekil formatinda deyil";
            }
            return result;
        }
        public static void FileDelete(string root, string folder, string image)
        {
            string filePath = Path.Combine(root, folder, image);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        
    }
}
