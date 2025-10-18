## What is ReactiveUI?

ReactiveUI gives you the power to build reactive, testable, and composable UI code using the MVVM pattern.

Handbook: [https://www.reactiveui.net/docs/handbook/](https://www.reactiveui.net/docs/handbook/)

(Basically it is a way to do cross platform MVVM)

## How to use this fork?

Right now adding ReactiveUI support to Myra requires modification to the original source code, so in order to use it, you need to clone the entire repo, switch to "reactive" branch, and build from source.

Steps:

0. Clone the repo, switch to reactive branch.
1. Open `build\Myra.PlatformAgnostic.sln`.
2. Build `Myra.PlatformAgnostic` and `Myra.ReactiveUI` project, just to verify it works.
3. Check sample project [Myra.Samples.ReactiveUI](samples/Myra.Samples.ReactiveUI) to see how to integrate and use ReactiveUI.
4. Add a reference to `Myra.PlatformAgnostic` and `Myra.ReactiveUI` project in your own project, and start coding.

Right now I only tested the PlatformAgnostic version, other version (like MonoGame version) should also work, but no promises!

To fit with Myra's event style, this integration comes with its own "Observable Events" implementation. So you don't need to use `ReactiveMarbles.ObservableEvents.SourceGenerator`.

## I try to run my code but my program breaks on this.WhenActivated/this.Bind

For some reason ReactiveUI cannot pick my integration automatically. You need to add these lines of code when program starts:

```csharp
using ReactiveUI.Myra;
using ReactiveUI.Builder;
using Splat;

var builder = new ReactiveUIBuilder(Locator.CurrentMutable, Locator.Current).WithMyra();
builder.BuildApp();
```

The best place to put all these are in the `Main(string[] args)` entry function, before creating Myra's `Desktop` object.

## What modification did you made to Myra?

Basically I just added `INotifyPropertyChanged` support to all UI widgets.

Also added ICommand support to `ButtonBase2` and `MenuItem`.

The ReactiveUI integration code was modified from ReactiveUI's official WinForm integration.
