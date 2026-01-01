import { DatePipe } from '@angular/common';
import { Injectable } from '@angular/core';
import { ConfigService } from './config.service';
import { SafeService } from './safe.service';
import { BaseService } from '../services/base.service';

@Injectable()

export class UtilityService extends BaseService {  
    
    constructor(private datepipe: DatePipe, private configService: ConfigService, private safeService: SafeService) {
        super();
    }

    displayDateOnly(created: string){
        return this.datepipe.transform(created, 'dd-MM-yyyy');
    }

    getCurrentDateAsDateOnly(){
        return this.datepipe.transform(new Date(), 'dd-MM-yyyy');
    }

    transformToSafeHtml(instructions: string){
      return this.safeService.transform(instructions);
    }

    removeHtml(instructions: string){
      return instructions
        .replace('&#x26;', '&')
        .replace('&#x3C;', '<')
        .replace('&#x3e;', '>')
        .replace('&#x22;', '"')
        .replace('&#x27;', "'")
        .replace('&nbsp;', ' ')
        .replace('&#xA9;', '©')
        .replace('&#xAE;', '®')
        .replace('&#x2122;', '™')

        .replace('&amp;', '&')
        .replace('&lt;', '<')
        .replace('&gt;', '>')
        .replace('&quot;', '"')
        .replace('&apos;', "'")
        .replace('&nbsp;', ' ')
        .replace('&copy;', '©')
        .replace('&reg;', '®')
        .replace('&trade;', '™')

        .replace('&#38;', '&')
        .replace('&#60;', '<')
        .replace('&#62;', '>')
        .replace('&#34;', '"')
        .replace('&#39;', "'")
        .replace('&#160;', ' ')
        .replace('&#169;', '©')
        .replace('&#174;', '®')
        .replace('&#8482;', '™')

        .replace(/(<([^>]+)>)/gi, '');
        // .replace(new RegExp('#([^\\s]*)','g'), '');
    }
}