﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace note__
{
    class CachedFile
    {
        private const int _buffSize = 30;
        private readonly string _baseName = "New File";
        private string _extension;
        private string _text;
        private string _tempName;
        private string _path;
        List<string> memory = Enumerable.Repeat("", _buffSize).ToList();
        private int _currentIndex = -1;
        private int _maxIndex = -1;

        /// <summary>
        /// Сохранить зменеия text в буфер.
        /// </summary>
        /// <param name="text"></param>
        public void SaveChange(string text)
        {
            if (CurrentIndex + 1 >= _buffSize)
            {
                memory.RemoveAt(0);
                memory.Add("");
                memory[CurrentIndex] = text;
            }
            else
            {
                CurrentIndex += 1;
                MaxIndex = CurrentIndex;
                memory[CurrentIndex] = text;
            }
            for (int i = CurrentIndex + 1; i < _buffSize; ++i)
            {
                memory[i] = memory[CurrentIndex];
            }
            Text = text;
        }

        /// <summary>
        /// Шаг назад.
        /// </summary>
        /// <returns> Текст на шаге назад. </returns>
        public string Undo()
        {
            CurrentIndex -= 1;
            Text = memory[CurrentIndex];
            return Text;
        }
        /// <summary>
        /// Шаг вперед.
        /// </summary>
        /// <returns> Текст на шаге вперед. </returns>
        public string Redo()
        {
            if (CurrentIndex < MaxIndex)
            {
                CurrentIndex += 1;
            }
            Text = memory[CurrentIndex];
            return Text;
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="cachedFiles"> cachedFiles для анализа имени. </param>
        /// <param name="extension"> Расширение файла. </param>
        public CachedFile(List<CachedFile> cachedFiles, string extension = ".txt")
        {
            _text = "";
            _tempName = GenerateName(cachedFiles);
            _extension = extension;
            _path = null;
            SaveChange(_text);
        }
        /// <summary>
        /// Конструктор для получепия файла по пути.
        /// </summary>
        /// <param name="path"> Путь до файла. </param>
        public CachedFile(string path)
        {
            _text = File.ReadAllText(path);
            _tempName = Path.GetFileName(path);
            _tempName = this._tempName[..this._tempName.LastIndexOf('.')];
            _extension = path[path.LastIndexOf('.')..];
            _path = path;
            SaveChange(_text);
        }
        /// <summary>
        /// Создание файла с заданным текстом и генерацией имени.
        /// </summary>
        /// <param name="text"> Нужнгый текст. </param>
        /// <param name="cachedFiles"> Файлы для анализа имени. </param>
        /// <param name="extension"> Расширение. </param>
        public CachedFile(string text, List<CachedFile> cachedFiles, string extension = ".txt")
        {
            _text = text;
            _tempName = GenerateName(cachedFiles);
            _extension = extension;
            _path = null;
            SaveChange(_text);
        }
        /// <summary>
        /// Создание файла с заданным массивом строк и генерацией имени.
        /// </summary>
        /// <param name="text"> Нужнгый текст. </param>
        /// <param name="cachedFiles"> Файлы для анализа имени. </param>
        /// <param name="extension"> Расширение. </param>
        public CachedFile(string[] text, List<CachedFile> cachedFiles, string extension = ".txt")
        {
            _text = String.Join("\n", text);
            _tempName = GenerateName(cachedFiles);
            _extension = extension;
            _path = null;
            SaveChange(_text);
        }
        /// <summary>
        /// Генератор имени.
        /// </summary>
        /// <param name="cachedFiles"> Файлы для анализа. </param>
        /// <returns> Строка - имя файла. </returns>
        public string GenerateName(List<CachedFile> cachedFiles)
        {
            string[] names = cachedFiles.Select(x => x.TempName).Where(x => x.Contains(_baseName)).Select(x => x.Replace(_baseName, "")).ToArray();
            for (int i = 1; i <= 10000; ++i)
            {
                if (!names.Contains(" " + i.ToString()))
                {
                    return _baseName + " " + i.ToString();
                }
            }
            return "blevota";

        }
        public string Text { get => _text; set { _text = value; } }
        public string Extension { get => _extension; set { _extension = value; } }
        public string TempName { get => _tempName; set { _tempName = value; } }
        public string FullName { get => _tempName + _extension; }
        public string FullPath
        {
            get => _path;
            set
            {
                _path = value;
            }
        }
        private int CurrentIndex
        {
            get
            {
                return _currentIndex;
            }
            set
            {
                if (value < 0)
                {
                    _currentIndex = 0;
                }
                else if (value > _buffSize - 1)
                {
                    _currentIndex = _buffSize - 1;
                }
                else
                {
                    _currentIndex = value;
                }
            }
        }
        private int MaxIndex
        {
            get
            {
                return _maxIndex;
            }
            set
            {
                if (value < 0)
                {
                    _maxIndex = 0;
                }
                else if (value > _buffSize - 1)
                {
                    _maxIndex = _buffSize - 1;
                }
                else
                {
                    _maxIndex = value;
                }
            }
        }
    }
}
