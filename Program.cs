using System;
using System.IO;
using System.Linq;
using System.Text;

namespace linq_slideviews
{
	public class Program
	{
		private static void Main()
		{
			var encoding = Encoding.GetEncoding(1251);
			var slideRecords = My_ParsingTask.ParseSlideRecords(File.ReadAllLines("slides.txt", encoding));
			var visitRecords = My_ParsingTask.ParseVisitRecords(File.ReadAllLines("visits.txt", encoding), slideRecords).ToList();
			foreach (var slideType in new[] { SlideType.Theory, SlideType.Exercise, SlideType.Quiz })
			{
				var time = My_StatisticsTask.GetMedianTimePerSlide(visitRecords, slideType);
				Console.WriteLine("Median time per slide '{0}': {1} mins", slideType, time);
			}
		}
	}
}
