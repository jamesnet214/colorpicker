﻿using ColorPicker.Support.Local.Models;
using Gma.System.MouseKeyHook;
using System;
using System.Drawing;
using System.Windows.Input;

namespace ColorPicker.Support.Local.Worker
{
    public class PixelExtractWorker
    {
        // 임시 버퍼의 graphic 타입
        private readonly Bitmap buffer = new(1, 1);
        private readonly Graphics buffer_graphics = null;
        private int count = 0;
        private IKeyboardMouseEvents globalMouseHook;
        public Action<ColorStruct> StartExtract = (p) => { };
        public Action FinishExtract = () => { };

        public PixelExtractWorker()
        {
            buffer_graphics = Graphics.FromImage(buffer);
        }

        public void Begin()
        {
            BeginCapture();
        }

        private void BeginCapture()
        {
            globalMouseHook = Hook.GlobalEvents();
            globalMouseHook.MouseMove += MainWindow_MouseMove;
            globalMouseHook.MouseDown += GlobalMouseHook_MouseDown;
            Mouse.OverrideCursor = Cursors.Cross;
        }

        private void GlobalMouseHook_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            CaptureFinished();
            FinishExtract();
        }

        private void CaptureFinished()
        {
            globalMouseHook.MouseMove -= MainWindow_MouseMove;
            globalMouseHook.MouseDown -= GlobalMouseHook_MouseDown;
            globalMouseHook.Dispose();
            Mouse.OverrideCursor = null;
        }

        private void MainWindow_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            SetView(e.X, e.Y);
        }

        private void SetView(int x, int y)
        {
            if (count > 3)
            {
                count = 0;
                ColorStruct color = new(ScreenColor(x, y));
                StartExtract(color);
            }
            else
            {
                count++;
            }
        }

        private Color ScreenColor(int x, int y)
        {
            // Mouse 위치의 색을 추출한다.
            buffer_graphics.CopyFromScreen(x, y, 0, 0, new Size(1, 1));
            // Pixel 값을 리턴한다.
            return buffer.GetPixel(0, 0);
        }
    }
}
