//
// File: "MemoryInfo.cs"
//
// Summary:
// Implement class representation of json returned by
// JavaScript "window.performance.memory".
//
// Revision History: 
// 2024/04/04:TomislavMatas: Version "1.0.0"
// * Initial version.
//

using System.Collections.Generic;

namespace EZSeleniumLib
{
    /// <summary>
    /// Class representation of json returned by
    /// JavaScript "window.performance.memory".
    /// </summary>
    public class MemoryInfo
    {
        /// <summary>
        /// "totalJsHeapSize" is current size of the JS heap in bytes
        /// including free space not occupied by any JS objects (yet).
        /// </summary>
        public long totalJSHeapSize;

        /// <summary>
        /// "usedJsHeapSize" is the total amount of memory in bytes 
        /// being used by JS objects including V8 internal objects.
        /// </summary>
        public long usedJSHeapSize;

        /// <summary>
        /// "jsHeapSizeLimit" is the maximum size of the heap, 
        /// in bytes, that is available to the context.
        /// This is the actual maximum amount of memory in bytes,
        /// that the JavaScript heap of a web application 
        /// is allowed to consume.
        /// </summary>
        public long jsHeapSizeLimit;

        /// <summary>
        /// Default consutructor. 
        /// </summary>
        public MemoryInfo() { }

        /// <summary>
        /// Custom constructor.
        /// Accepts object json returned by
        /// JavaScript "window.performance.memory".
        /// </summary>
        /// <param name="dict"></param>
        public MemoryInfo(object obj)
        {
            this.InitFromObject(obj);
        }

        /// <summary>
        /// Custom constructor.
        /// Accepts a dictonary which should contain keys 
        /// named exactly as the property names of this class.
        /// </summary>
        /// <param name="dictionary"></param>
        public MemoryInfo(Dictionary<string, object> dictionary)
        {
            this.InitFromDictionary(dictionary);
        }

        /// <summary>
        /// Implements a "cheap" json serialization. 
        /// </summary>
        /// <returns></returns>
        public string ToJsonString(bool pretty=false) {
#if DEBUG
            pretty=true;
#endif
            string sepRoot = (pretty ? "\r\n" : string.Empty);
            string sepElem = ( pretty ? "\r\n ": " ");
            string json =
              sepRoot + @"{" + sepElem 
              + string.Format("{0}: {1},", nameof(totalJSHeapSize), totalJSHeapSize) + sepElem
              + string.Format("{0}: {1},", nameof(usedJSHeapSize),  usedJSHeapSize)  + sepElem
              + string.Format("{0}: {1} ", nameof(jsHeapSizeLimit), jsHeapSizeLimit) 
              + sepRoot + @"}";
            return json;
        }

        private void InitFromObject(object obj)
        {
            this.InitFromDictionary((Dictionary<string, object>)obj);
        }

        private void InitFromDictionary(Dictionary<string, object> dict)
        {
            if (dict == null)
                return;

            if (dict.TryGetValue(nameof(totalJSHeapSize), out object _totalJSHeapSize))
                if (_totalJSHeapSize != null)
                    this.totalJSHeapSize = (long)_totalJSHeapSize;

            if (dict.TryGetValue(nameof(usedJSHeapSize), out object _usedJSHeapSize))
                if (_usedJSHeapSize != null)
                    this.usedJSHeapSize = (long)_usedJSHeapSize;

            if (dict.TryGetValue(nameof(jsHeapSizeLimit), out object _jsHeapSizeLimit))
                if (_jsHeapSizeLimit != null)
                    this.jsHeapSizeLimit = (long)_jsHeapSizeLimit;
        }

    } // class

} // namespace
