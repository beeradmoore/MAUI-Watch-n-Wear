//
//  ContentView.swift
//  watchApp WatchKit Extension
//
//  Created by Csaba Huszár on 2022. 06. 25..
//  Copyright © 2022. orgName. All rights reserved.
//

import SwiftUI
import watchshared

struct ContentView: View {
    let greet = Greeting().greeting()

    var body: some View {
        Text(greet)
    }
}

struct ContentView_Previews: PreviewProvider {
    static var previews: some View {
        ContentView()
    }
}
