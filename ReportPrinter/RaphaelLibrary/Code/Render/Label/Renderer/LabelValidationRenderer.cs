using System;
using System.Collections.Generic;
using RaphaelLibrary.Code.Init.Label;
using RaphaelLibrary.Code.Init.SQL;
using RaphaelLibrary.Code.Render.Label.Helper;
using RaphaelLibrary.Code.Render.Label.Model;
using RaphaelLibrary.Code.Render.Label.PlaceHolder;
using RaphaelLibrary.Code.Render.PDF.Model;
using ReportPrinterLibrary.Code.Enum;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.Label.Renderer
{
    public class LabelValidationRenderer : LabelRendererBase
    {
        public LabelValidationRenderer(int lineIndex) : base(lineIndex) { }

        protected override bool TryCreatePlaceHolders(LabelDeserializeHelper deserializer, HashSet<string> placeholders)
        {
            var procName = $"{this.GetType().Name}.{nameof(TryCreatePlaceHolders)}";

            foreach (var placeholder in placeholders)
            {
                if (!deserializer.TryGetValue(placeholder, LabelElementHelper.S_TYPE, out var validationTypeStr) || 
                    !Enum.TryParse(validationTypeStr, out ValidationType validationType))
                {
                    Logger.LogMissingFieldLog(LabelElementHelper.S_TYPE, placeholder, procName);
                    return false;
                }

                if (!deserializer.TryGetValue(placeholder, LabelElementHelper.S_COMPARATOR, out var comparatorStr) ||
                    !Enum.TryParse(comparatorStr, out Comparator comparator))
                {
                    Logger.LogMissingFieldLog(LabelElementHelper.S_COMPARATOR, placeholder, procName);
                    return false;
                }

                if (!deserializer.TryGetValue(placeholder, LabelElementHelper.S_EXPECTED_VALUE, out var expectedValue))
                {
                    Logger.LogMissingFieldLog(LabelElementHelper.S_EXPECTED_VALUE, placeholder, procName);
                    return false;
                }

                if (!deserializer.TryGetValue(placeholder, LabelElementHelper.S_SQL_TEMPLATE_ID, out var sqlTemplateId) || string.IsNullOrEmpty(sqlTemplateId))
                {
                    Logger.LogMissingFieldLog(LabelElementHelper.S_SQL_TEMPLATE_ID, placeholder, procName);
                    return false;
                }

                if (!deserializer.TryGetValue(placeholder, LabelElementHelper.S_SQL_ID, out var sqlId) || string.IsNullOrEmpty(sqlId))
                {
                    Logger.LogMissingFieldLog(LabelElementHelper.S_SQL_ID, placeholder, procName);
                    return false;
                }

                if (!SqlTemplateManager.Instance.TryGetSql(sqlTemplateId, sqlId, out var sql))
                    return false;

                if (!deserializer.TryGetValue(placeholder, LabelElementHelper.S_SQL_RES_COLUMN, out var sqlResColumn) || string.IsNullOrEmpty(sqlResColumn))
                {
                    Logger.LogMissingFieldLog(LabelElementHelper.S_SQL_RES_COLUMN, placeholder, procName);
                    return false;
                }

                if (!deserializer.TryGetValue(placeholder, LabelElementHelper.S_TRUE_STRUCTURE, out var trueContent) || string.IsNullOrEmpty(trueContent))
                {
                    Logger.LogMissingFieldLog(LabelElementHelper.S_TRUE_STRUCTURE, placeholder, procName);
                    return false;
                }

                if (!deserializer.TryGetValue(placeholder, LabelElementHelper.S_FALSE_STRUCTURE, out var falseContent) || string.IsNullOrEmpty(falseContent))
                {
                    Logger.LogMissingFieldLog(LabelElementHelper.S_FALSE_STRUCTURE, placeholder, procName);
                    return false;
                }

                if (validationType == ValidationType.Text)
                {
                    var validationMode = new ValidationModel(validationType, comparator, expectedValue, trueContent, falseContent);
                    var validationPlaceholder = new ValidationPlaceHolder(placeholder, sql, new SqlResColumn(sqlResColumn), validationMode);
                    PlaceHolders.Add(validationPlaceholder);
                }
                else if (validationType == ValidationType.Structure)
                {
                    if (!LabelStructureManager.Instance.TryGetLabelStructure(trueContent, out var trueStructure))
                        return false;

                    if (!LabelStructureManager.Instance.TryGetLabelStructure(falseContent, out var falseStructure))
                        return false;

                    var validationModel = new ValidationModel(validationType, comparator, expectedValue, trueStructure, falseStructure);
                    var validationPlaceholder = new ValidationPlaceHolder(placeholder, sql, new SqlResColumn(sqlResColumn), validationModel);
                    PlaceHolders.Add(validationPlaceholder);
                }
            }

            return true;
        }
    }
}