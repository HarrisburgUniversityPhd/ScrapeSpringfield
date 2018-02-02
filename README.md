# Introduction

Pulls down movie/TV scripts from the site [Springfield !Springfield!][springfield] by filing type for a given range.

# How to use

1. Open a command prompt
2. Change to the directory where you downloaded the files
3. Run `ScrapeSpringfield.exe`

# Parameters

`ScrapeSpringfield.exe` has 4 optional parameters

1. type
  * The type of scripts to download.
  * Options: movie, tv 
  * Defaults to movie
  * Alias: t
2. start
  * The first letter of the show's name from which to start the pull (inclusive).
  * Defaults to 0.
  * Alias: s
3. end
  * The first letter of the show's name on which to end the pull (inclusive).
  * Defaults to Z.
  * Alias: e
4. path
  * The path to save files to.
  * Defaults to %Desktop%/springfield
  * Alias: p

```{shell}
ScrapeSpringfield.exe
```

---------

[springfield]: https://www.springfieldspringfield.co.uk/
