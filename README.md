# Overview

This repo is the ported version of the given example of [ImagePicker Android library(https://github.com/nguyenhoanglam/ImagePicker) built by [Nguyen Hoang Lam](https://github.com/nguyenhoanglam).

In this repo, we have two examples
- One for Xamarin.Android
- One for .NET 8 Android

# How to use the package in your project

## 1/ Install the nuget package

```
<PackageReference Include="Com.Github.Nguyenhoanglam.ImagePicker" Version="1.6.2" />
```

If your installation fails, you will need to correct the dependencies. In my case, I have to install the following package for my .NET 8 project.

```
<PackageReference Include="Xamarin.Kotlin.StdLib.Jdk7" Version="1.9.21.1" />
```

## Amend your `CSPROJ` file to include these following lines

```
<ItemGroup>
  <GradleRepository Include="https://jitpack.io">
    <Repository>
    maven {
        url 'https://jitpack.io'
    }
    </Repository>
  </GradleRepository>
</ItemGroup>
```

We need this because the binding library is using the new way of downloading its native artifact from Maven repository.

# Known issues

## 1/ .NET8 Android::Cannot install the nuget package - RESOLVED

Check carefully the output window for what is the actual error.

In my case, it's about package version conflict because Nuget Package Manager cannot handle well when package A use vX.1 of package C but package B uses vX.2 of package C. We have to install the higher version of the dependency first, before we can install our package.

## 2/ Xamarin.Android::Cannot run due to JDK version issue - TRIAGING

[Related issue](https://issuetracker.google.com/issues/212279104?pli=1)

```
Caused by: com.android.tools.r8.internal.Hc: Sealed classes are not supported as program classes
	at com.android.tools.r8.graph.r2.c(R8_3.3.75_b7a6ff6b13548611571508fe72282c9167faa649161ca0013edfc92e19bd7e58:4)
	at com.android.tools.r8.internal.Pa.a(R8_3.3.75_b7a6ff6b13548611571508fe72282c9167faa649161ca0013edfc92e19bd7e58:349)
	at com.android.tools.r8.graph.u2.a(R8_3.3.75_b7a6ff6b13548611571508fe72282c9167faa649161ca0013edfc92e19bd7e58:42)
	at com.android.tools.r8.internal.Fj.a(R8_3.3.75_b7a6ff6b13548611571508fe72282c9167faa649161ca0013edfc92e19bd7e58:101)
	at com.android.tools.r8.internal.Fj.a(R8_3.3.75_b7a6ff6b13548611571508fe72282c9167faa649161ca0013edfc92e19bd7e58:102)
	at com.android.tools.r8.internal.Fj.a(R8_3.3.75_b7a6ff6b13548611571508fe72282c9167faa649161ca0013edfc92e19bd7e58:100)
```

## 3/ Gradle build fails - RESOLVED
If you see anything in the console relates to `xgradle`, please try to **clean and rebuild** until it goes away.

```
Error	GDL004	Failed to create directory '{your-folder-path}\Net8AndroidImagePickerQs\obj\xgradle'.	Net8AndroidImagePickerQs
C:\Users\{your-user-name}\.nuget\packages\dependencies.gradle\7.6.1\buildTransitive\Dependencies.Gradle.targets	30		
```

# License

This repository is published under [MIT license](./LICENSE). You are freely to fork, use in your projects regardless of its purpose.