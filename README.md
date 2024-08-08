# Quest Text Editor

A tool to edit the Mobile version of Pokémon Quest's encrypted TextAssets.

![Picture showing the application](./Screenshots/Example.png "Quest Text Editor")<br>

## Features

### Editor

A full editor to change any label of an imported Pokémon Quest TextAsset.
- Import a UABEA exported .bytes file from the TextAsset in an AssetBundle.
- Select a label to edit in the list and then change its contents and its UserParam.
- The editor automatically re-calculates the Metadata of the file so the labels are read properly.
- Rebuilding the binary file is done in "non-coded" mode, so the data within the file is raw UTF-8 encoded strings.
- The exported binary file can be re-imported in UABEA.

### Text Exporter

If you just want to export the text into a basic .txt file, you can!
- Each line of the file is a label, with nothing else special.

## Special Thanks

- ­[xiofee](https://github.com/xiofee) for the original [Pokémon Quest Text Decoder](https://github.com/xiofee/Pokemon-Quest-Text-Decoder) from which this repo is forked.