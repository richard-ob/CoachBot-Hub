

export default class ColourUtils {
    static isDark(hexColour: string) {
        const color = (hexColour.charAt(0) === '#') ? hexColour.substring(1, 7) : hexColour;
        const r = parseInt(color.substring(0, 2), 16);
        const g = parseInt(color.substring(2, 4), 16);
        const b = parseInt(color.substring(4, 6), 16);

        return (((r * 0.299) + (g * 0.587) + (b * 0.114)) > 186);
    }

    static hexToRgbA(hex: string, opacity: number = 1, ignoreAlpha: boolean = false) {
        let colour;
        if (/^#([A-Fa-f0-9]{3}){1,2}$/.test(hex)) {
            colour = hex.substring(1).split('');
            if (colour.length === 3) {
                colour = [colour[0], colour[0], colour[1], colour[1], colour[2], colour[2]];
            }
            colour = '0x' + colour.join('');

            if (ignoreAlpha) {
                // tslint:disable-next-line:no-bitwise
                return 'rgb(' + [(colour >> 16) & 255, (colour >> 8) & 255, colour & 255].join(',') + ')';
            } else {
                // tslint:disable-next-line:no-bitwise
                return 'rgba(' + [(colour >> 16) & 255, (colour >> 8) & 255, colour & 255].join(',') + ', ' + opacity + ')';
            }
        }
    }
    
    static hexToRgbArray(hex: string) {
        let colour;
        if (/^#([A-Fa-f0-9]{3}){1,2}$/.test(hex)) {
            colour = hex.substring(1).split('');
            if (colour.length === 3) {
                colour = [colour[0], colour[0], colour[1], colour[1], colour[2], colour[2]];
            }
            colour = '0x' + colour.join('');

            // tslint:disable-next-line:no-bitwise
            return [(colour >> 16) & 255, (colour >> 8) & 255, colour & 255];
        }
    }

    static getNormalisedSecondColour(colourOneHex: string, colourTwoHex: string) {
        const colourOneRgb = this.hexToRgbArray(colourOneHex);
        const colourTwoRgb = this.hexToRgbArray(colourTwoHex);
        const deltaE = this.deltaE(colourOneRgb, colourTwoRgb);

        console.log(deltaE);

        if (deltaE < 10) {
            return '#38a9ff';
        } else {
            return colourTwoHex;
        }
    }

    static deltaE(rgbA, rgbB) {
        let labA = this.rgb2lab(rgbA);
        let labB = this.rgb2lab(rgbB);
        let deltaL = labA[0] - labB[0];
        let deltaA = labA[1] - labB[1];
        let deltaB = labA[2] - labB[2];
        let c1 = Math.sqrt(labA[1] * labA[1] + labA[2] * labA[2]);
        let c2 = Math.sqrt(labB[1] * labB[1] + labB[2] * labB[2]);
        let deltaC = c1 - c2;
        let deltaH = deltaA * deltaA + deltaB * deltaB - deltaC * deltaC;
        deltaH = deltaH < 0 ? 0 : Math.sqrt(deltaH);
        let sc = 1.0 + 0.045 * c1;
        let sh = 1.0 + 0.015 * c1;
        let deltaLKlsl = deltaL / (1.0);
        let deltaCkcsc = deltaC / (sc);
        let deltaHkhsh = deltaH / (sh);
        let i = deltaLKlsl * deltaLKlsl + deltaCkcsc * deltaCkcsc + deltaHkhsh * deltaHkhsh;
        return i < 0 ? 0 : Math.sqrt(i);
      }
      
      static rgba2hex(orig) {
        var a, isPercent,
          rgb = orig.replace(/\s/g, '').match(/^rgba?\((\d+),(\d+),(\d+),?([^,\s)]+)?/i),
          alpha = (rgb && rgb[4] || "").trim(),
          hex = rgb ?
          (rgb[1] | 1 << 8).toString(16).slice(1) +
          (rgb[2] | 1 << 8).toString(16).slice(1) +
          (rgb[3] | 1 << 8).toString(16).slice(1) : orig;
      
        if (alpha !== "") {
          a = alpha;
        } else {
          a = 0o1;
        }
        // multiply before convert to HEX
        a = ((a * 255) | 1 << 8).toString(16).slice(1)
        hex = hex + a;
      
        return hex;
      }
      
      private static rgb2lab(rgb){
        let r = rgb[0] / 255, g = rgb[1] / 255, b = rgb[2] / 255, x, y, z;
        r = (r > 0.04045) ? Math.pow((r + 0.055) / 1.055, 2.4) : r / 12.92;
        g = (g > 0.04045) ? Math.pow((g + 0.055) / 1.055, 2.4) : g / 12.92;
        b = (b > 0.04045) ? Math.pow((b + 0.055) / 1.055, 2.4) : b / 12.92;
        x = (r * 0.4124 + g * 0.3576 + b * 0.1805) / 0.95047;
        y = (r * 0.2126 + g * 0.7152 + b * 0.0722) / 1.00000;
        z = (r * 0.0193 + g * 0.1192 + b * 0.9505) / 1.08883;
        x = (x > 0.008856) ? Math.pow(x, 1/3) : (7.787 * x) + 16/116;
        y = (y > 0.008856) ? Math.pow(y, 1/3) : (7.787 * y) + 16/116;
        z = (z > 0.008856) ? Math.pow(z, 1/3) : (7.787 * z) + 16/116;
        return [(116 * y) - 16, 500 * (x - y), 200 * (y - z)]
      }
}
