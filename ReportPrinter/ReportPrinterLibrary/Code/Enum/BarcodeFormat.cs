namespace ReportPrinterLibrary.Code.Enum
{
    public enum BarcodeFormat : int
    {
        AZTEC = 1,
        CODABAR = 2,
        CODE_39 = 4,
        CODE_93 = 8,
        CODE_128 = 16, // 0x00000010
        DATA_MATRIX = 32, // 0x00000020
        EAN_8 = 64, // 0x00000040
        EAN_13 = 128, // 0x00000080
        ITF = 256, // 0x00000100
        MAXICODE = 512, // 0x00000200
        PDF_417 = 1024, // 0x00000400
        QR_CODE = 2048, // 0x00000800
        RSS_14 = 4096, // 0x00001000
        RSS_EXPANDED = 8192, // 0x00002000
        UPC_A = 16384, // 0x00004000
        UPC_E = 32768, // 0x00008000
        UPC_EAN_EXTENSION = 65536, // 0x00010000
        MSI = 131072, // 0x00020000
        PLESSEY = 262144, // 0x00040000
        IMB = 524288, // 0x00080000
        PHARMA_CODE = 1048576, // 0x00100000
        All_1D = UPC_E | UPC_A | RSS_EXPANDED | RSS_14 | ITF | EAN_13 | EAN_8 | CODE_128 | CODE_93 | CODE_39 | CODABAR, // 0x0000F1DE
    }
}