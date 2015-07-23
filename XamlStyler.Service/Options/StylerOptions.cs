﻿using System.ComponentModel;

namespace XamlStyler.Core.Options
{
    public class StylerOptions : IStylerOptions
    {
        public StylerOptions()
        {
            // Initialize all properties with "DefaultValueAttrbute" to their default value
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(this))
            {
                // Set default value if DefaultValueAttribute is present
                DefaultValueAttribute attr = prop.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
                if (attr != null)
                    prop.SetValue(this, attr.Value);
            }
        }

        //Indentation

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue(2)]
        [Browsable(false)]
        public int IndentSize { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue(false)]
        [Browsable(false)]
        public bool IndentWithTabs { get; set; }

        // Attribute formatting

        [Category("Attribute formatting")]
        [DisplayName("Attribute tolerance")]
        [Description("Defines the attribute number tolerance before XamlStyler starts to break attributes into new lines. A value less than 1 meaning no tolerance.\r\ne.g., when this setting is 2\r\n\r\nBEFORE BEAUTIFY:\r\n<TextBlock x:Name=\"m_sample\"\r\n    Text=\"asdf\" />\r\n\r\nAFTER BEAUTIFY:\r\n<TextBlock x:Name=\"m_sample\" Text=\"asdf\" />\r\nDefault Value: 2")]
        [DefaultValue(2)]
        public int AttributesTolerance { get; set; }

        [Category("Attribute formatting")]
        [DisplayName("Position first attribute on same line as start tag")]
        [Description("Defines whether the first line of attribute(s) shall appear on the same line as the element's start tag.\r\ne.g., when this setting is true\r\n\r\nBEFORE BEAUTIFY:\r\n\"<element a='xyz' b='xyz'>  </element>\"\r\n\r\nAFTER BEAUTIFY:\r\n\"<element a='xyz'\r\n        b='xyz'>\r\n</element>\"\r\n\r\nDefault Value: true")]
        [DefaultValue(true)]
        public bool KeepFirstAttributeOnSameLine { get; set; }

        [Category("Attribute formatting")]
        [DisplayName("Max attribute characters per line")]
        [Description("Defines the maximum charater length (not including indentation characters) of attributes an element can have on each line after the start tag. A value less than 1 meaning no limit.\r\nDefault Value: 0")]
        [DefaultValue(0)]
        public int MaxAttributeCharatersPerLine { get; set; }

        [Category("Attribute formatting")]
        [DisplayName("Max attributes per line")]
        [Description("Defines the maximum number of attributes an element can have on each line after the start tag. A value less than 1 meaning no limit.\r\nDefault Value: 1")]
        [DefaultValue(1)]
        public int MaxAttributesPerLine { get; set; }

        [Category("Attribute formatting")]
        [DisplayName("Elements no line break between attributes")]
        [Description("Defines a list of elements whose attributes shall not be broken into lines.\r\nDefault Value: RadialGradientBrush, GradientStop, LinearGradientBrush, ScaleTransfom, SkewTransform, RotateTransform, TranslateTransform, Trigger, Setter")]
        [DefaultValue("RadialGradientBrush, GradientStop, LinearGradientBrush, ScaleTransfom, SkewTransform, RotateTransform, TranslateTransform, Trigger, Condition, Setter")]
        public string NoNewLineElements { get; set; }

        // Attribute Reordering

        [Category("Attribute Reordering")]
        [DisplayName("Enable Attribute Ordering")]
        [Description("If this is disabled, attributes will not be reordered in any way.")]
        [DefaultValue(true)]
        public bool EnableAttributeReordering { get; set; }

        [Category("Attribute Reordering")]
        [DisplayName("Attribute Ordering Rule Groups")]
        [Description("Defines attribute ordering rule groups. Each string element is one group. \r\nUse ',' to seperate more than one attribute.'DOS' wildcards are allowed. \r\nAttributes listed in earlier groups takes precedence over later groups. \r\nAttributes listed earlier in same group takes precedence over the ones listed later.")]
        [DefaultValue(new[]
        {
            // Class definition group
            "x:Class",
            // WPF Namespaces group
            "xmlns, xmlns:x",
            // Other namespace
            "xmlns:*",
            // Element key group
            "Key, x:Key, Uid, x:Uid",
            // Element name group
            "Name, x:Name, Title",
            // Attached layout group
            "Grid.Row, Grid.RowSpan, Grid.Column, Grid.ColumnSpan, Canvas.Left, Canvas.Top, Canvas.Right, Canvas.Bottom",
            // Core layout group
            "Width, Height, MinWidth, MinHeight, MaxWidth, MaxHeight, Margin",
            // Alignment layout group
            "HorizontalAlignment, VerticalAlignment, HorizontalContentAlignment, VerticalContentAlignment, Panel.ZIndex",
            // Unmatched
            "*:*, *",
            // Miscellaneous/Other attributes group
            "PageSource, PageIndex, Offset, Color, TargetName, Property, Value, StartPoint, EndPoint",
            // Blend related group
            "mc:Ignorable, d:IsDataSource, d:LayoutOverrides, d:IsStaticText",
        })]
        public string[] AttributeOrderingRuleGroups { get; set; }

        [Category("Attribute Reordering")]
        [DisplayName("Order attributes by name when not matched by a rule group")]
        [Description("Sort attributes by name (attribute ordering rule groups takes precedence)")]
        [DefaultValue(true)]
        public bool OrderAttributesByName { get; set; }

        [Category("Attribute Reordering")]
        [DisplayName("Put attributes order rule groups on separate lines")]
        [Description("Put attributes belonging to different groups on different lines (keep identical groups on same line if possible)")]
        [DefaultValue(false)]
        public bool PutAttributeOrderRuleGroupsOnSeparateLines { get; set; }

        // Element formatting

        [Category("Element formatting")]
        [DisplayName("Put ending bracket on new line")]
        [Description("Defines whether to put \">\" or \"/>\" on a new line.\r\nDefault Value: false")]
        [DefaultValue(false)]
        public bool PutEndingBracketOnNewLine { get; set; }

        [Category("Element formatting")]
        [DisplayName("Remove ending tag of empty element")]
        [Description("Defines whether to remove the ending tag of an empty element.\r\ne.g., when this setting is true\r\n\r\nBEFORE BEAUTIFY:\r\n\"<element>  </element>\"\r\n\r\nAFTER BEAUTIFY:\r\n\"<element />\"\r\n\r\nDefault Value: true")]
        [DefaultValue(true)]
        public bool RemoveEndingTagOfEmptyElement { get; set; }

        [Category("Element formatting")]
        [DisplayName("Space before closing slash in self closing element")]
        [Description("Defines whether to have a space before slash in self closing elements \r\ne.g., when\r\ntrue <br />\r\nfalse <br/>\r\n\r\nDefault Value: true")]
        [DefaultValue(true)]
        public bool SpaceBeforeClosingSlash { get; set; }

        [Category("Element formatting")]
        [DisplayName("Root element line breaks between attributes")]
        [Description("Defines if attributes of the document root element are broken into separate lines or not.\r\nDefault = use same rules as other elements")]
        [DefaultValue(LineBreakRule.Default)]
        public LineBreakRule RootElementLineBreakRule { get; set; }

        // Element reordering

        [Category("Element Reordering")]
        [DisplayName("Reorder Grid panel children by row/column")]
        [Description("Defines whether to reorder the children of a Grid by row/column.  When this is true, children will be reordered in an ascending fashion by looking at their attached Grid properties: first by Grid.Row, then by Grid.Column.")]
        [DefaultValue(true)]
        public bool ReorderGridChildren { get; set; }

        [Category("Element Reordering")]
        [DisplayName("Reorder Canvas panel children by left/top/right/bottom")]
        [Description("Defines whether to reorder the children of a Canvas by left/top/right/bottom.  When this is true, children will be reordered in an ascending fashion by looking at their attached Canvas properties: first by Canvas.Left, then by Canvas.Top, then by Canvas.Right, then by Canvas.Bottom.")]
        [DefaultValue(true)]
        public bool ReorderCanvasChildren { get; set; }

        [Category("Element Reordering")]
        [DisplayName("Reorder Setters by")]
        [Description("Defines whether to reorder 'Setter' elements in style/trigger elements. When this is set, children will be reordered in an ascending fashion by looking at their Property and/or TargetName properties")]
        [DefaultValue(ReorderSettersBy.None)]
        public ReorderSettersBy ReorderSetters { get; set; }

        //Markup Extension

        [Category("Markup Extension")]
        [DisplayName("Enable Markup Extension Formatting")]
        [Description("Defines whether to format markup extensions (attributes containg '{}'). \r\nDefalut Value: true \r\nWhen this setting is true, attributes with markup extensions will always be put on a new line, UNLESS the element is under AttributesTolerance or one of the NoNewLineElements.")]
        [DefaultValue(true)]
        public bool FormatMarkupExtension { get; set; }

        [Category("Markup Extension")]
        [DisplayName("Keep x:Bind statements on same line")]
        [Description("Defines whether the attribute(s) of an x:Bind statement will be kept on the same line \r\n\r\nDefault Value: true")]
        [DefaultValue(true)]
        public bool KeepxBindOnSameLine { get; set; }

        [Category("Markup Extension")]
        [DisplayName("Keep Binding statements on same line")]
        [Description("Defines whether the attribute(s) of a Binding statement will be kept on the same line \r\n\r\nDefault Value: false")]
        [DefaultValue(false)]
        public bool KeepBindingsOnSameLine { get; set; }

        // Misc

        [Category("Misc")]
        [DisplayName("Beautify on saving xaml")]
        [Description("Defines whether to automatically beautify the active xaml document while saving.")]
        [DefaultValue(true)]
        public bool BeautifyOnSave { get; set; }
    }
}