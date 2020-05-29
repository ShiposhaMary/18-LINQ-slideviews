﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace linq_slideviews
{
	public class My_ParsingTask
	{
		/// <param name="lines">все строки файла, которые нужно распарсить. Первая строка заголовочная.</param>
		/// <returns>Словарь: ключ — идентификатор слайда, значение — информация о слайде</returns>
		/// <remarks>Метод должен пропускать некорректные строки, игнорируя их</remarks>
		public static IDictionary<int, SlideRecord> ParseSlideRecords(IEnumerable<string> lines)
		{

            return
                lines.Skip(1)
                    .Select(line => line.Split(';'))
                    .Select(CheckExceptionSlide)
                    .Where(slide => slide != null)
                    .ToDictionary(slide => slide.SlideId);
        }
        private static SlideRecord CheckExceptionSlide(string[] data)
        {
            if (data.Length != 3)
                return null;

            int id;
            if (!int.TryParse(data[0], out id))
                return null;

            SlideType type;
            if (!Enum.TryParse(data[1], true, out type))
                return null;

            return new SlideRecord(id, type, data[2]);
        }

        public static IEnumerable<VisitRecord> ParseVisitRecords(
            IEnumerable<string> lines, IDictionary<int, SlideRecord> slides)
        {
            return lines.Skip(1).Select(line => CheckExceptionVisit(slides, line));
        }

        /// <param name="lines">все строки файла, которые нужно распарсить. Первая строка — заголовочная.</param>
        /// <param name="slides">Словарь информации о слайдах по идентификатору слайда. 
        /// Такой словарь можно получить методом ParseSlideRecords</param>
        /// <returns>Список информации о посещениях</returns>
        /// <exception cref="FormatException">Если среди строк есть некорректные</exception>
        private static VisitRecord CheckExceptionVisit(IDictionary<int, SlideRecord> slides, string line)
        {
            try
            {
                var data = line.Split(';');
                var slideId = int.Parse(data[1]);
                return new VisitRecord(int.Parse(data[0]),slideId, DateTime.ParseExact(
                        data[2] + ' ' + data[3],
                        "yyyy-MM-dd HH:mm:ss",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None), slides[slideId].SlideType);
            }
            catch (Exception e)
            {
                throw new FormatException($"Wrong line [{line}]", e);
            }
        }
    }	
}