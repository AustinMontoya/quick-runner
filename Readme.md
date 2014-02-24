# QuickRunner

QuickRunner is a parallel nunit runner with the goal of "casual" parallelism to make the tests run faster (as opposed to PNunit, which has the opposite goal). It's useful when:

- Your tests take a really long time to run (e.g. Selenium Tests)
- You need to run tests against multiple environments
- You have tests which run against a persisted datastore that would fail if being run normally in parallel.

## How does it work?

QuickRunner takes in a config file that specifies:

- The assembly you would like to test against (required)
- The target environments to run the tests against (at least one required)
- Options for reporting and division of the tests

First, a folder is created for each environment containing a copy of the assembly you want to test and its dependencies.

It then divides up the tests, spawns an nunit-console for each specified environment, runs them, and optionally aggregates the results into a single file.

Currently, specifying multiple environments is the only way in which to run your tests in parallel, though there are plans to create "workers", similar to the upcoming nunit workers feature, in the near future. Environments, however, are completely arbitrary and only require a name, so parallelism can be enabled simply by declaring a list of environments equal to number of consoles you would like to run.

## Options

There are both global and environment-level options that can be specified to tell QuickRunner what it's supposed to do

### Global

- `AssemblyPath` (**required**): The path to the *folder* (not the file itself) containing the assembly
- `AssemblyFileName` (**required**): The name of the assembly to test against
- `SplitTestsBy` (optional, defaults to "method"): Tells QuickRunner how to divide all of the tests. The two available options are `namespace` and `method`. `namespace` keeps all tests contained in a namespace together to run in one environment. `method` splits the individual tests evenly across all environments.
- `AggregateResults` (optional, defaults to `false`): Indicates whether to create a single output file for results across all environments. The advantage of using this over a CI plugin is that an extra attribute is added to each <test-case> element, "qr-environment", which specifies which environment the particular case ran in.
- `ResultsFilepath` (optional, defaults to "$currentDirectory\TestResults.xml"): Specifies an alternate location and filename to save aggregated results to. Is ignored if `AggregateResults` is set to false or omitted.
- `ConfigFilepath` (optional): Specifies a path, relative to the AssemblyPath, where a file containing <app-settings> is stored.


### Environment-Specific
- `Name` (**required**): Specifies the name of the environment. Should be unique.
- `AppSettings` (optional): Overwrites or extends the copy of <app-settings> created when this environment is copied from the AssemblyPath. <add> elements will be created or overwritten based on the key-value pairs provided here. Note that this will probably become more flexible in the future, allowing for multiple config sections to be specified and overridden.

## Example

Using the Slowlenium project included in this repo is a good way to get started:

```javascript
{
	"AssemblyPath": "C:\\dev\\quick-runner\\Slowlenium\\bin\\Debug", // assuming this is where the project is on your local filesystem
	"AssemblyFileName": "Slowlenium.dll",
	"SplitTestsBy": "namespace",
	"AggregateResults": true,
	"ResultsFilepath": "C:\\temp\\test-results.xml",
	"Environments": [
		{
			"Name": "carp",
		},
		{
			"Name": "catfish",
		}
	]
}
```

This will create two "environments" that run the tests in parallel. You'll see that it cuts the running time significantly over running via a single instance of nunit-console or nunit-gui.

## Hints

- Do not try and increase the number of environments to be equal to the number of tests, as this will cause a degradation in performance. This is because there is a couple seconds of overhead to start nunit-console.