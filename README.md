# Bluebeam CSV Summary Reformatter
A console application to reformat Bluebeam CSV markup summaries into a more traditional comment sheet format where responses are recorded in a single column as opposed to a new row.

![Comment sheet reformat - Before and After](https://github.com/einajade-forest/bluebeam-reformat-comments/blob/cc4edd7b9e203ad17d2b54019be78455f376e8b9/snapshots/BBSR_Reformat.PNG)

## Purpose of this project
This is a personal project born from the desire to practise coding. The application is inspired by a previous experience with transitioning users from paper to digital PDF reviews and frequent requests for compromise during the early adoption phase. This was an experiement as to whether it is possible to efficiently reformat the Bluebeam CSV markup summaries, rather than a tool that aimed to advance the original PDF markup software.

## How does it work?
1. Console application analyses the Markups Summary CSV file and checks that all required fields have been included in the export from Bluebeam
2. If one does not already exist, a custom "Response" column header is created
3. Each of the remaining rows in the CSV file is read and categorised as a _Comment_ or _Reply_
4. Nested replies are identified through the ID and Parent columns and consolidated into a single _response_ property in the parent _Reply_
5. The remaining top level replies are then captured in their corresponding _Comment_'s _response_ property
6. The finalised collection of _Comment_ is written as a new CSV file in the application's Export folder

## Included files
This repository contains:
- C# files
- [Resources.resx](https://github.com/einajade-forest/bluebeam-reformat-comments/blob/0870549925af32c40534217b8348b9322b5206b2/Properties/Resources.resx), containing static messages to be written to the console
- [Sample Bluebeam Markup Summary CSV export](https://github.com/einajade-forest/bluebeam-reformat-comments/blob/e51a6f13e11ae38e1430932e7c88ac10a65e3afd/sample/sampleMarkupSummary.csv)

## Licence
[MIT License](https://github.com/einajade-forest/bluebeam-reformat-comments/blob/e51a6f13e11ae38e1430932e7c88ac10a65e3afd/LICENSE)

