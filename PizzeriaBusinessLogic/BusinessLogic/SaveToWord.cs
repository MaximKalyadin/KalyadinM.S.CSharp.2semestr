using System;
using System.Collections.Generic;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using PizzeriaBusinessLogic.HelperModels;

namespace PizzeriaBusinessLogic.BusinessLogic
{
    static class SaveToWord
    {
        public static void CreateDoc(WordInfo info)
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(info.FileName, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body docBody = mainPart.Document.AppendChild(new Body());

                docBody.AppendChild(CreateParagraph(new WordParagraph
                {
                    Texts = new List<string> { info.Title },
                    TextProperties = new WordParagraphProperties
                    {
                        Bold = true,
                        Size = "24",
                        JustificationValues = JustificationValues.Center
                    }
                }));

                foreach (var pizza in info.Pizzas)
                {
                    docBody.AppendChild(CreateParagraph(new WordParagraph
                    {
                        Texts = new List<string> { pizza.PizzaName, " - " + pizza.Price.ToString() },
                        TextProperties = new WordParagraphProperties
                        {
                            Size = "24",
                            JustificationValues = JustificationValues.Both
                        }
                    }));
                }

                docBody.AppendChild(CreateSectionProperties());
                wordDocument.MainDocumentPart.Document.Save();
            }
        }

        public static void CreateDoc(WordInfoSklad info)
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(info.FileName, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body docBody = mainPart.Document.AppendChild(new Body());
                docBody.AppendChild(CreateParagraph(new WordParagraph
                {
                    Texts = new List<string> { info.Title },
                    TextProperties = new WordParagraphProperties
                    {
                        Bold = true,
                        Size = "24",
                        JustificationValues = JustificationValues.Center
                    }
                }));
                Table table = docBody.AppendChild(new Table());
                TableProperties tblProperties = new TableProperties();

                TableBorders tblBorders = new TableBorders();
                TopBorder topBorder = new TopBorder();
                topBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                topBorder.Color = "CC0000";
                tblBorders.AppendChild(topBorder);

                BottomBorder bottomBorder = new BottomBorder();
                bottomBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                bottomBorder.Color = "CC0000";
                tblBorders.AppendChild(bottomBorder);

                RightBorder rightBorder = new RightBorder();
                rightBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                rightBorder.Color = "CC0000";
                tblBorders.AppendChild(rightBorder);

                LeftBorder leftBorder = new LeftBorder();
                leftBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                leftBorder.Color = "CC0000";
                tblBorders.AppendChild(leftBorder);

                InsideHorizontalBorder insideHBorder = new InsideHorizontalBorder();
                insideHBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                insideHBorder.Color = "CC0000";
                tblBorders.AppendChild(insideHBorder);

                InsideVerticalBorder insideVBorder = new InsideVerticalBorder();
                insideVBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                insideVBorder.Color = "CC0000";
                tblBorders.AppendChild(insideVBorder);

                tblProperties.AppendChild(tblBorders);
                table.AppendChild(tblProperties);
                foreach (var sklad in info.Sklads)
                {
                    table.AppendChild(new TableRow(new TableCell(CreateParagraph(new WordParagraph
                    {
                        Texts = new List<string> { sklad.SkladName },
                        TextProperties = new WordParagraphProperties
                        {
                            Bold = false,
                            Size = "24",
                            JustificationValues = JustificationValues.Center
                        }
                    }))));
                }
                docBody.AppendChild(CreateSectionProperties());
                wordDocument.MainDocumentPart.Document.Save();
            }
        }

        private static SectionProperties CreateSectionProperties()
        {
            SectionProperties properties = new SectionProperties();
            PageSize pageSize = new PageSize
            {
                Orient = PageOrientationValues.Portrait
            };
            properties.AppendChild(pageSize);
            return properties;
        }

        private static Paragraph CreateParagraph(WordParagraph paragraph)
        {
            if (paragraph != null)
            {
                Paragraph docParagraph = new Paragraph();

                docParagraph.AppendChild(CreateParagraphProperties(paragraph.TextProperties));
                for (int i = 0; i < paragraph.Texts.Count; i++)
                {
                    {
                        Run docRun = new Run();
                        RunProperties properties = new RunProperties();
                        properties.AppendChild(new FontSize
                        {
                            Val = paragraph.TextProperties.Size
                        });
                        if (i == 0)
                            properties.AppendChild(new Bold());
                        docRun.AppendChild(properties);
                        docRun.AppendChild(new Text
                        {
                            Text = paragraph.Texts[i],
                            Space = SpaceProcessingModeValues.Preserve
                        });
                        docParagraph.AppendChild(docRun);
                    }
                }
                return docParagraph;
            }
            return null;
        }

        private static ParagraphProperties CreateParagraphProperties(WordParagraphProperties paragraphProperties)
        {
            if (paragraphProperties != null)
            {
                ParagraphProperties properties = new ParagraphProperties();
                properties.AppendChild(new Justification()
                {
                    Val = paragraphProperties.JustificationValues
                });
                properties.AppendChild(new SpacingBetweenLines
                {
                    LineRule = LineSpacingRuleValues.Auto
                });
                properties.AppendChild(new Indentation());
                ParagraphMarkRunProperties paragraphMarkRunProperties = new ParagraphMarkRunProperties();
                if (!string.IsNullOrEmpty(paragraphProperties.Size))
                {
                    paragraphMarkRunProperties.AppendChild(new FontSize { Val = paragraphProperties.Size });
                }
                if (paragraphProperties.Bold)
                {
                    paragraphMarkRunProperties.AppendChild(new Bold());
                }
                properties.AppendChild(paragraphMarkRunProperties);
                return properties;
            }
            return null;
        }
    }
}
