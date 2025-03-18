using System;
using System.Threading.Tasks;
using Avalonia.Reactive;
using Tmds.DBus.SourceGenerator;

namespace Avalonia.FreeDesktop.DBusIme.Fcitx
{
    internal class FcitxICWrapper
    {
        private readonly OrgFcitxFcitxInputContext1Proxy? _modern;
        private readonly OrgFcitxFcitxInputContextProxy? _old;

        public FcitxICWrapper(OrgFcitxFcitxInputContextProxy old)
        {
            _old = old;
        }

        public FcitxICWrapper(OrgFcitxFcitxInputContext1Proxy modern)
        {
            _modern = modern;
        }

        public Task FocusInAsync() => _old?.FocusInAsync() ?? _modern?.FocusInAsync() ?? Task.CompletedTask;

        public Task FocusOutAsync() => _old?.FocusOutAsync() ?? _modern?.FocusOutAsync() ?? Task.CompletedTask;

        public Task ResetAsync() => _old?.ResetAsync() ?? _modern?.ResetAsync() ?? Task.CompletedTask;

        public Task SetCursorRectAsync(int32 x, int32 y, int32 w, int32 h) =>
            _old?.SetCursorRectAsync(x, y, w, h) ?? _modern?.SetCursorRectAsync(x, y, w, h) ?? Task.CompletedTask;

        public Task DestroyICAsync() => _old?.DestroyICAsync() ?? _modern?.DestroyICAsync() ?? Task.CompletedTask;

        public async Task<bool> ProcessKeyEventAsync(uint32 keyVal, uint32 keyCode, uint32 state, int32 type, uint32 time)
        {
            if (_old is not null)
                return await _old.ProcessKeyEventAsync(keyVal, keyCode, state, type, time) != 0;
            return await (_modern?.ProcessKeyEventAsync(keyVal, keyCode, state, type > 0, time) ?? Task.FromResult(false));
        }

        public ValueTask<IDisposable> WatchCommitStringAsync(Action<Exception?, string> handler) =>
            _old?.WatchCommitStringAsync(handler)
            ?? _modern?.WatchCommitStringAsync(handler)
            ?? new ValueTask<IDisposable>(Disposable.Empty);

        public ValueTask<IDisposable> WatchForwardKeyAsync(Action<Exception?, (uint32 keyval, uint32 state, int32 type)> handler) =>
            _old?.WatchForwardKeyAsync(handler)
            ?? _modern?.WatchForwardKeyAsync((e, ev) => handler.Invoke(e, (ev.Keyval, ev.State, ev.Type ? 1 : 0)))
            ?? new ValueTask<IDisposable>(Disposable.Empty);

        public ValueTask<IDisposable> WatchUpdateFormattedPreeditAsync(
            Action<Exception?, ((string?, int32)[]? str, int32 cursorpos)> handler) =>
            _old?.WatchUpdateFormattedPreeditAsync(handler!)
            ?? _modern?.WatchUpdateFormattedPreeditAsync(handler!)
            ?? new ValueTask<IDisposable>(Disposable.Empty);

        public Task SetCapacityAsync(uint32 flags) =>
            _old?.SetCapacityAsync(flags) ?? _modern?.SetCapabilityAsync(flags) ?? Task.CompletedTask;
    }
}
