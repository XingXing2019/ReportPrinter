using System.Collections.Generic;

namespace RaphaelLibrary.Code.Render.Label.Helper
{
    public class LabelElementHelper
    {
        public const string S_START = "Start";
        public const string S_REFERENCE = "Reference";
        public const string S_SQL = "Sql";
        public const string S_SQL_VARIABLE = "SqlVariable";
        public const string S_TIMESTAMP = "Timestamp";
        public const string S_VALIDATION = "Validation";
        public const string S_END = "End";

        public const string S_RENDERER_START = "%%%<";
        public const string S_REFERENCE_RENDERER = "%%%<Reference";
        public const string S_SQL_RENDERER = "%%%<Sql";
        public const string S_SQL_VARIABLE_RENDERER = "%%%<SqlVariable";
        public const string S_TIMESTAMP_RENDERER = "%%%<Timestamp";
        public const string S_VALIDATION_RENDERER = "%%%<Validation";
        public const string S_RENDERER_END = "/>%%%";

        public const string S_NEW_LINE = "\n";
        public const string S_DOUBLE_QUOTE = "\"";

        public const string S_COMPARATOR = "Comparator";
        public const string S_EXPECTED_VALUE = "ExpectedValue";
        public const string S_FALSE_STRUCTURE = "FalseStructure";
        public const string S_IS_UTC = "IsUTC";
        public const string S_MASK = "Mask";
        public const string S_NAME = "Name";
        public const string S_SQL_ID = "SqlId";
        public const string S_SQL_RES_COLUMN = "SqlResColumn";
        public const string S_SQL_TEMPLATE_ID = "SqlTemplateId";
        public const string S_STRUCTURE_ID = "StructureId";
        public const string S_TRUE_STRUCTURE = "TrueStructure";
        public const string S_TYPE = "Type";



        public static Dictionary<string, string> LABEL_RENDERER = new Dictionary<string, string>
        {
            { S_START, S_RENDERER_START },
            { S_REFERENCE, S_REFERENCE_RENDERER },
            { S_SQL, S_SQL_RENDERER },
            { S_SQL_VARIABLE, S_SQL_VARIABLE_RENDERER },
            { S_TIMESTAMP, S_TIMESTAMP_RENDERER },
            { S_VALIDATION, S_VALIDATION_RENDERER },
            { S_END, S_RENDERER_END }
        };
    }
}