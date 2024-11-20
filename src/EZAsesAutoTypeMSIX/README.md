# "EZAsesAutoTypeMSIX.wapproj"
The project is used to build the MSIX deployment and publishing package.
For details visit -->< https://learn.microsoft.com/de-de/windows/msix/desktop/vs-package-overview >

# Troubleshooting

## Build-Failure "MSB3270: There was a mismatch ..." 
Complete Error-Message during deployment of package:
"MSB3270: There was a mismatch between the processor architecture of the 
project being built 'architecture' and the processor architecture of the 
reference 'dependency', 'architecture'. This mismatch may cause runtime 
failures. Please consider changing the targeted processor architecture 
of your project through the Configuration Manager so as to align the 
processor architectures between your project and references, or take a 
dependency on references with a processor architecture that matches the 
targeted processor architecture of your project."

Solution:
See: -->< https://learn.microsoft.com/de-de/visualstudio/msbuild/errors/msb3270?view=vs-2022 >
Adding "ResolveAssemblyWarnOrErrorOnTargetArchitectureMismatch=None" 
to all the project files of projects that reported the failure,
does not actualy solve the issue. That "supresses" the error only.
Instead, make sure to add the the property "PlatformTarget" 
with the same value for, e.G.: 
```xml
<...>
<PropertyGroup>
   <...>
   <PlatformTarget>AnyCPU</PlatformTarget>
   <...>
</PropertyGroup>
<...>
```

# Revision History
## 2024/11/20:TomislavMatas: Version "1.131.1"
* WIP: Prototyping.
* Add section "Troubleshooting" to this "README.md" file.

## 2024/04/07:TomislavMatas: Version "1.124.0"
* Initial version.
