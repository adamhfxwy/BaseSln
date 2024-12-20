﻿namespace LanTian.Solution.Core.CommonHelper
{
    public class GpsUtils
    {
        private static double x_pi = 3.14159265358979324 * 3000.0 / 180.0;
        // π
        private static double pi = 3.1415926535897932384626;
        // 长半轴
        private static double a = 6378245.0;
        // 扁率
        private static double ee = 0.00669342162296594323;
        public static bool OutOfChina(double lon, double lat)
        {
            if (lon < 72.004 || lon > 137.8347)
            {
                return true;
            }
            else if (lat < 0.8293 || lat > 55.8271)
            {
                return true;
            }
            return false;
        }
        public static double TransFormlat(double lon, double lat)
        {
            double ret = -100.0 + 2.0 * lon + 3.0 * lat + 0.2 * lat * lat + 0.1 * lon * lat +
           0.2 * Math.Sqrt(Math.Abs(lon));
            ret += (20.0 * Math.Sin(6.0 * lon * pi) + 20.0 * Math.Sin(2.0 * lon * pi)) * 2.0 /
           3.0;
            ret += (20.0 * Math.Sin(lat * pi) + 40.0 * Math.Sin(lat / 3.0 * pi)) * 2.0 / 3.0;
            ret += (160.0 * Math.Sin(lat / 12.0 * pi) + 320 * Math.Sin(lat * pi / 30.0)) * 2.0 /
           3.0;
            return ret;
        }
        public static double TransFormlng(double lon, double lat)
        {
            double ret = 300.0 + lon + 2.0 * lat + 0.1 * lon * lon + 0.1 * lon * lat + 0.1 *
           Math.Sqrt(Math.Abs(lon));
            ret += (20.0 * Math.Sin(6.0 * lon * pi) + 20.0 * Math.Sin(2.0 * lon * pi)) * 2.0 /
           3.0;
            ret += (20.0 * Math.Sin(lon * pi) + 40.0 * Math.Sin(lon / 3.0 * pi)) * 2.0 / 3.0;
            ret += (150.0 * Math.Sin(lon / 12.0 * pi) + 300.0 * Math.Sin(lon / 30.0 * pi)) * 2.0
           / 3.0;
            return ret;
        }
        /**
        * WGS84转GCJ02(火星坐标系)
        *
        * @param wgs_lon WGS84坐标系的经度
        * @param wgs_lat WGS84坐标系的纬度
        * @return 火星坐标数组
        */
        /// <summary> 
        /// WGS84转GCJ02(火星坐标系)
        /// </summary> 
        /// <param name=""></param> 
        /// <returns></returns> 
        public static double[] Wgs84ToGcj02(double wgs_lon, double wgs_lat)
        {
            if (OutOfChina(wgs_lon, wgs_lat))
            {
                return new double[] { wgs_lon, wgs_lat };
            }
            double dlat = TransFormlat(wgs_lon - 105.0, wgs_lat - 35.0);
            double dlng = TransFormlng(wgs_lon - 105.0, wgs_lat - 35.0);
            double radlat = wgs_lat / 180.0 * pi;
            double magic = Math.Sin(radlat);
            magic = 1 - ee * magic * magic;
            double sqrtmagic = Math.Sqrt(magic);
            dlat = (dlat * 180.0) / ((a * (1 - ee)) / (magic * sqrtmagic) * pi);
            dlng = (dlng * 180.0) / (a / sqrtmagic * Math.Cos(radlat) * pi);
            double mglat = wgs_lat + dlat;
            double mglng = wgs_lon + dlng;
            return new double[] { mglng, mglat };
        }
        /**
        * GCJ02(火星坐标系)转GPS84
        *
        * @param gcj_lon 火星坐标系的经度
        * @param gcj_lat 火星坐标系纬度
        * @return WGS84坐标数组
        */
        /// <summary> 
        /// GCJ02(火星坐标系)转GPS84
        /// </summary> 
        /// <param name=""></param> 
        /// <returns></returns> 
        public static double[] Gcj02ToWgs84(double gcj_lon, double gcj_lat)
        {
            if (OutOfChina(gcj_lon, gcj_lat))
            {
                return new double[] { gcj_lon, gcj_lat };
            }
            double dlat = TransFormlat(gcj_lon - 105.0, gcj_lat - 35.0);
            double dlng = TransFormlng(gcj_lon - 105.0, gcj_lat - 35.0);
            double radlat = gcj_lat / 180.0 * pi;
            double magic = Math.Sin(radlat);
            magic = 1 - ee * magic * magic;
            double sqrtmagic = Math.Sqrt(magic);
            dlat = (dlat * 180.0) / ((a * (1 - ee)) / (magic * sqrtmagic) * pi);
            dlng = (dlng * 180.0) / (a / sqrtmagic * Math.Cos(radlat) * pi);
            double mglat = gcj_lat + dlat;
            double mglng = gcj_lon + dlng;
            return new double[] { gcj_lon * 2 - mglng, gcj_lat * 2 - mglat };
        }
        /**
        * 火星坐标系(GCJ-02)转百度坐标系(BD-09)
        *
        * 谷歌、高德——>百度
        * @param gcj_lon 火星坐标经度
        * @param gcj_lat 火星坐标纬度
        * @return 百度坐标数组
        */
        /// <summary> 
        /// 火星坐标系(GCJ-02)转百度坐标系(BD-09)
        /// </summary> 
        /// <param name=""></param> 
        /// <returns></returns> 
        public static double[] Gcj02ToBd09(double gcj_lon, double gcj_lat)
        {
            double z = Math.Sqrt(gcj_lon * gcj_lon + gcj_lat * gcj_lat) + 0.00002 *
           Math.Sin(gcj_lat * x_pi);
            double theta = Math.Atan2(gcj_lat, gcj_lon) + 0.000003 * Math.Cos(gcj_lon * x_pi);
            double bd_lng = z * Math.Cos(theta) + 0.0065;
            double bd_lat = z * Math.Sin(theta) + 0.006;
            return new double[] { bd_lng, bd_lat };
        }
        /**
        * 百度坐标系(BD-09)转火星坐标系(GCJ-02)
        *
        * 百度——>谷歌、高德
        * @param bd_lon 百度坐标纬度
        * @param bd_lat 百度坐标经度
        * @return 火星坐标数组
        */
        /// <summary> 
        /// 百度坐标系(BD-09)转火星坐标系(GCJ-02)
        /// </summary> 
        /// <param name=""></param> 
        /// <returns></returns> 
        public static double[] Bd09ToGcj02(double bd_lon, double bd_lat)
        {
            double x = bd_lon - 0.0065;
            double y = bd_lat - 0.006;
            double z = Math.Sqrt(x * x + y * y) - 0.00002 * Math.Sin(y * x_pi);
            double theta = Math.Atan2(y, x) - 0.000003 * Math.Cos(x * x_pi);
            double gg_lng = z * Math.Cos(theta);
            double gg_lat = z * Math.Sin(theta);
            return new double[] { gg_lng, gg_lat };
        }
        /**
        * WGS坐标转百度坐标系(BD-09)
        *
        * @param wgs_lng WGS84坐标系的经度
        * @param wgs_lat WGS84坐标系的纬度
        * @return 百度坐标数组
        */
        /// <summary> 
        /// WGS坐标转百度坐标系(BD-09)
        /// </summary> 
        /// <param name=""></param> 
        /// <returns></returns> 
        public static double[] Wgs84ToBd09(double wgs_lng, double wgs_lat)
        {
            double[] gcj = Wgs84ToGcj02(wgs_lng, wgs_lat);
            double[] bd09 = Gcj02ToBd09(gcj[0], gcj[1]);
            return bd09;
        }
        /**
 * 百度坐标系(BD-09)转WGS坐标
 *
 * @param bd_lng 百度坐标纬度
 * @param bd_lat 百度坐标经度
 * @return WGS84坐标数组
 */
        /// <summary> 
        ///  百度坐标系(BD-09)转WGS坐标
        /// </summary> 
        /// <param name=""></param> 
        /// <returns></returns> 
        public static double[] Bd09ToWgs84(double bd_lng, double bd_lat)
        {
            double[] gcj = Bd09ToGcj02(bd_lng, bd_lat);
            double[] wgs84 = Gcj02ToWgs84(gcj[0], gcj[1]);
            return wgs84;
        }
    }

}
