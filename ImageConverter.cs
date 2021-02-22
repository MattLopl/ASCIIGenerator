﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ASCIIGenerator
{
  public class ImageConverter
  {
    public Luminances Config { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="config"></param>
    public ImageConverter(Luminances config)
    {
      Config = config;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="resizedImage"></param>
    /// <returns></returns>
    public string ReadPixels(Bitmap resizedImage)
    {
      //Reads dimensions of image.
      int imageHeight = resizedImage.Height -1;
      int imageWidth = resizedImage.Width -1;

      //Initialises a StringBuilder which allows the ASCII characters to be written to a string for saving (without taking up too much memory!)
      var saveToString = new StringBuilder();

      //Reads every pixel in the resized image.
      for (int heightCounter = 0; heightCounter <= imageHeight; heightCounter++)
      {
        for (int widthCounter = 0; widthCounter <= imageWidth; widthCounter++)
        {
          //Uses the pixel to generate an ASCII character. Writes two to console, as characters are twice as tall as they are wide.
          var charValue = SelectASCII(resizedImage, widthCounter, heightCounter);
          Console.Write(charValue + charValue);

          //Adds characters to string for saving
          saveToString.Append(charValue + charValue);
        }
        //Moves to the next row of pixels in both the console rendering, and the string for saving.
        Console.WriteLine();
        saveToString.AppendLine();
      }
      return saveToString.ToString();
    }

    /// <summary>
    /// Takes the RGB values of the selected pixel, and uses it to calculate luminance. Selects the appropriate ASCII character for the luminance value and returns it.
    /// </summary>
    /// <param name="resizedImage"> The Bitmap image being read. </param>
    /// <param name="widthCounter"> The x value of the selected pixel. </param>
    /// <param name="heightCounter"> The y value of the selected pixel. </param>
    /// <returns></returns>
    private string SelectASCII(Bitmap resizedImage, int widthCounter, int heightCounter)
    {
      //Gets the RGB of the current pixel.
      Color pixel = resizedImage.GetPixel(widthCounter, heightCounter);
      //Uses a known approximation to calculate luminance (see readme.txt)
      double luminance = (0.375 * pixel.R) + (0.5 * pixel.G) + (0.125 * pixel.B);

      //Casts the selected luminance value to an int (and normalises within the luminance range, just in case)
      int index = (int)luminance;
      if (index < 0 && index > Config.Configuration.Count)
      {
        if (index < 0) index = 0;
        else index = 255;
      }
      //Looks up the relevant ASCII character and returns it.
      var charValue = Config.Configuration[index].CharValue;
      return charValue;
    }
  }
}
