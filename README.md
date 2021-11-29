# Bluebeam CSV Summary Reformatter
A Console Application to reformat Bluebeam CSV markup summaries into a more traditional comment sheet format.

## Purpose of this project
This is a personal project born from the desire to practise coding. The application is inspired by a previous experience with transitioning users from paper to digital PDF reviews and frequent requests for compromise during the early adoption phase. This was an experiement as to whether it is possible to efficiently reformat the Bluebeam CSV markup summaries, rather than a tool that aims to advance the original PDF markup software.

## How does it work?
1. After, exporting the Markups Summary in XSV format from Bluebeam, launch application
2. Enter full file path of the markup summary to be reformatted
3. Column headers are checked to ensure all required fields have been exported from Bluebeam
4. Rows are read and categorised as _comments_ or _replies_
5. Nested replies are identified through the ID and Parent columns and consolidated into a single _response_ property

## Included files
This repository contains:
- C# files
- [Resources.resx](https://github.com/einajade-forest/bluebeam-reformat-comments/blob/0870549925af32c40534217b8348b9322b5206b2/Properties/Resources.resx), containing static messages to be written to the console
- [Sample Bluebeam Markup Summary CSV export](https://github.com/einajade-forest/bluebeam-reformat-comments/blob/e51a6f13e11ae38e1430932e7c88ac10a65e3afd/sample/sampleMarkupSummary.csv)

## Licence
[MIT License](https://github.com/einajade-forest/bluebeam-reformat-comments/blob/e51a6f13e11ae38e1430932e7c88ac10a65e3afd/LICENSE)

