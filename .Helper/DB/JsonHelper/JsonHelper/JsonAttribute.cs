using System;
using System.Linq;
using System.IO;
using System.Buffers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DB.JsonHelper
{
    public interface IReadAttribute
    {
        bool AllowTrailingCommas{get;}
        int ReadComment{get;}
        int MaxDepth{get;}
    }
    public interface IWriteAttribute
    {
        bool AutoSpace{get;}
        bool SkipValid{get;}
    }

    public class ReadAttribute:IReadAttribute
    {    
        protected JsonReaderOptions options;  

        /// <summary>
        /// 要读取的 JSON 有效负载中是否允许（和忽略）对象或数组中 JSON 值的列表末尾多余的逗号
        /// </summary>
        /// <value></value>
        public bool AllowTrailingCommas
        {
            get => options.AllowTrailingCommas;
        }

        /// <summary>
        /// 是否忽略注释
        /// Disallow  0 	不允许在 JSON 输入中使用注释。 若找到注释，可将其视为 JSON，并且引发 JsonException。 这是默认值。
        /// Skip 	  1 	允许在 JSON 输入中使用注释并忽略它们。 Utf8JsonReader 的行为方式假设不存在注释。
        /// Allow 	  2 	允许在 JSON 输入中使用注释，并将其视为有效标记。 读取项时，调用方可以访问注释值。
        /// </summary>
        /// <value></value>
        public int ReadComment
        {
            get => (int)options.CommentHandling;
        }

        /// <summary>
        /// 迭代最大深度深度
        /// </summary>
        /// <value></value>
        public int MaxDepth
        {
            get => options.MaxDepth;
        }

        public ReadAttribute()
        {
            options = default;
        }
        public ReadAttribute(bool allowTrailingCommas,int readComment,int maxDepth)
        {
            options = new JsonReaderOptions
            {
                AllowTrailingCommas = allowTrailingCommas,
                CommentHandling = (JsonCommentHandling )readComment,
                MaxDepth = maxDepth
            };
        }
    }

    public class WriteAttribute:IWriteAttribute
    {
        protected JsonWriterOptions options; 	
        public bool AutoSpace{get=>options.Indented;}
        public bool SkipValid{get=>options.SkipValidation;}

        public WriteAttribute()
        {
            options = default;
        }

        public WriteAttribute(bool autoSpace,bool skipValid)
        {
            options = new JsonWriterOptions
            {
                Indented = autoSpace,
                SkipValidation = skipValid
            };
        }

    }
}