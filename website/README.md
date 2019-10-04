# VegaIT Front-end HBS Starter

*"Live In Your World, Code In Ours."*



## Installation Instructions

Install dependencies by running Node.js package manager.

    npm install

Launch *development build* Gulp task.

    gulp build-dev


## Gulp Tasks

### Starting Dev Mode

To start *watch mode*, execute `gulp` task.

    gulp


### Creating a Module

To create a new module, execute `gulp cf` task and pass `-m` with argument.

    gulp cf -m some-module

This command will create new module files in `src/html/modules` directory:
* .hbs
* .json

And also a file in `src/scss/modules` directory:
* .scss

It will also update `style.scss` file in `src/scss` directory.

If you use `isMultilanguage` global variable, this will create all the additional languaes you defined in `globalVars.languages`

### Creating a Template

To create a new template, execute `gulp cf` task and pass `-t` with argument.

    gulp cf -t some-template

This command will create new template files in `src/html/templates` directory:
* .hbs
* .json

If you use `isMultilanguage` global variable, this will create all the additional languaes you defined in `globalVars.languages`

### Generate Font Icons

To generate font icons, execute `gulp iconfont` task.

    gulp iconfont

This command will generate fonts:
* .ttf
* .woff
* .woff2

in `dist/assets/fonts` directory based on svg files from `src/assets/svg` directory.

It will also update `_icon-font.scss` file in `src/scss/layout` directory.

See that file for css classes you can use to display font icons.

In order to show icons, all you need to do is add class `"icon font-ico-heart"`

    <span class="icon font-ico-heart"></span>


### Building Files

To clean up dist folder (remove it), execute `gulp reset-dev`. This is helpful when many images are updated or removed, until dist is cleaned up, build task will not remove these images.

    gulp reset-dev

To create development version files, execute `gulp build-dev` task (noindex, nofollow + NOT minified css and js).

    gulp build-dev

To create stage version files, execute `gulp build-stage` task (noindex, nofollow + minified css and js).

    gulp build-stage

To create production version files, execute `gulp build-prod` task (index, follow, GTM + minified css and js).

    gulp build-prod

## Using Files

### HBS
#### Modules
All sections, modules, blocks, ... *(header, footer, banner...)* should be created as modules in `src/html/modules` and `src/scss/modules` directory.

Each module has three files:

* `src/html/modules` directory:
    * .hbs *(module html templating structure)*
    * .json *(module content)*

* `src/scss/modules` directory:
    * .scss *(module styles)*

**Example:**

*.hbs file:*
```
<div class="team-list">
    <h2>{{title}}</h2>

    {{#if people}}
        <ul>
            {{#each people}}
                <li>{{this.name}} {{this.surname}}</li>
            {{/each}}
        </ul>
    {{/if}}
</div><!-- .team-list -->
```

*.json file:*
```
{
    "title": "Team List",
    "people": [
        {
            "name": "Emiko",
            "surname": "Groce"
        },
        {
            "name": "Leonila",
            "surname": "Gillins"
        }
    ]
}
```

*This will compile into:*
```
<div class="team-list">
    <h2>Team List</h2>

    <ul>
        <li>Emiko Groce</li>
        <li>Leonila Gillins</li>
    </ul>
</div>
<!-- .team-list -->
```

See [Handlebars](https://handlebarsjs.com/) templating engine for more information about `.hbs` files.

#### Templates

Ideally all Templates should be created using *Modules*.

Each template has two files:
* .hbs *(template html templating structure)*
* .json *(template content)*

**Example:**

*.hbs file:*
```
{{#> master data }}
    {{#*inline "template-content"}}

        {{> header header}}

        {{> basic-block basicBlock}}

        {{> basic-block basicBlock2}}

        {{> basic-block basicBlock3}}

        {{> footer footer}}

    {{/inline}}
{{/master}}
```
*All page content **MUST** be inside `{{#HTML data}}{{/HTML}}` tag with `data` parameter*.

*.json file:*
```
{
    "template": "about",
    "data": {
        "header": {
            ">>": "header/data.json"
        },
        "footer": {
            ">>": "footer/data.json"
        },
        "basicBlock": {
            ">>": "basic-block/data.json"
        },
        "basicBlock2": {
        "title": "This diferent Title",
        "content": "<p>Lorem ipsudddm dolor sit amet, consectetur adipisicing elit, sed do eiusmodtempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam,quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodoconsequat.</p><p>Duis aute irure dolor in reprehenderit in voluptate velit essecillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat nonproident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>"
        },
        "basicBlock3": {
            ">>": "basic-block/data.json",
            "title": "This is changed Title"
        }
    }
}
```

- Use `template` prop to set the name of compiled file *(`"template": "about"` will create `about.html`)*

- Use `data` prop object to:
- **include** module's `data.json` file:
    ```
    "header": {
        ">>": "header/data.json"
    }
    ```
- or **input** different data:
    ```
    "basicBlock2": {
        "title": "This diferent Title",
        "content": "<p>Lorem ipsudddm dolor sit amet, consectetur adipisicing elit, sed do eiusmodtempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam,quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodoconsequat.</p><p>Duis aute irure dolor in reprehenderit in voluptate velit essecillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat nonproident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>"
    }
    ```

- or **include** modules's `data.json` file and override **only** data you need:
    ```
    "basicBlock3": {
        ">>": "basic-block/data.json",
        "title": "This is changed Title"
    }
    ```

*This will compile into an `.html` file in `dist` directory.

#### CONDITIONS

The `compare` helper can be used where truthy or falsy data values are not enough, but you instead like to compare two data values, or compare something against a static value.

It supports all common operators, like `===`, `!==`, `<`, `<=`, `>`, `>=`, `&&` and `||`.

**Example:**

```
{{#if (compare v1 'operator' v2) }}
    foo
{{else if (compare v1 'operator' v2) }}
    bar
{{else}}
    baz
{{/if}}
```

Inline (nested) Condition Usage:

```
{{#if (compare (compare v1 'operator' v2) 'operator' (compare v1 'operator' v2))}}
    foo
{{else}}
    bar
{{/if}}
```


### SCSS
All styles should be written in `src/scss` directories.

CSS code quality is checked with [Sass Lint](https://github.com/sasstools/sass-lint)


## Config files

### Global Variables
In this file are some variables that are used for or control the behavior of gulp tasks.
Path: src/config/gulp-tasks/_global-vars.js

### isBeta
By default its value is set to `false` and it should be unchanged unless you are working on Emperor projects.
Changing its value to `true` and re-running build task will create a button in the upper right corner, by clicking the button a popup will be opened. Running `gulp build-prod` will automatically change its value to `false` and the button as well as the popup will not be rendered.
This should tell the client that the project is still in working phase and changes may occur.

### isMultilanguage
If you select this option hbs will get generated into separate folder for all of the languages.
**Important!** use this only if its a static website or an annual report.
To add more languages just updated the `globalVars.languages` array with new language.
